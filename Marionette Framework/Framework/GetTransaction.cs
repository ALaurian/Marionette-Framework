// Namespace for the Marionette Framework

using Marionette.Orchestrator;
using Marionette.Orchestrator.Enums;

namespace Marionette_Framework;

// Class for the Framework
partial class Framework
{
    // Method for getting a transaction from the orchestrator queue
    public static void GetTransaction(int in_TransactionNumber, Dictionary<string, object> in_Config,
        out QueueItem out_TransactionItem, out string out_TransactionID, Orchestrator OrchestratorConnection)
    {
        // Log message to console
        Console.WriteLine("Get the transaction item.");

        // Set the transaction item to null
        out_TransactionItem = null;

        // Set retry flag to true and retries counter to 0
        bool retry = true;
        int retries = 0;

        // Get queue from Orchestrator and convert it to a list of queue items
        var orchestratorQueue = OrchestratorConnection.ReceiveData(in_Config["OrchestratorQueueName"].ToString());
        var queueItems = OrchestratorConnection.ConvertDataTableToQueueItemList(orchestratorQueue);

        while (retry)
        {
            // If the requested transaction number is equal to the number of queue items, break out of loop
            if (in_TransactionNumber == queueItems.Count)
                break;

            try
            {
                // Fetch queue items in order of priority
                var highPriorityItems = queueItems.Where(q =>
                    q.Priority == QueueItemPriority.High && q.Status == QueueItemStatus.New);
                var mediumPriorityItems = queueItems.Where(q =>
                    q.Priority == QueueItemPriority.Medium && q.Status == QueueItemStatus.New);
                var lowPriorityItems = queueItems.Where(q =>
                    q.Priority == QueueItemPriority.Low && q.Status == QueueItemStatus.New);

                // Concatenate the queue items in order of priority
                var orderedQueueItems = highPriorityItems.Concat(mediumPriorityItems).Concat(lowPriorityItems);

                // Retrieve the transaction item with highest priority that is in "New" status
                out_TransactionItem = orderedQueueItems.FirstOrDefault();

                // If the transaction item with the highest priority is found, break out of loop
                if (out_TransactionItem != null)
                    break;

                // If no transaction items with "New" status are found, increment the transaction number and continue loop
                in_TransactionNumber++;
            }
            catch (Exception e)
            {
                // Set retry flag to true and log exception message to console
                retry = true;
                Console.WriteLine($"Could not retrieve transaction item. Exception message: {e.Message}");

                // If this is the last retry attempt, rethrow the exception
                if (retries == Int32.Parse(in_Config["RetryNumberGetTransactionItem"].ToString()) - 1)
                {
                    throw;
                }
            }
        }


        // Assign current timestamp to out_TransactionID if out_TransactionItem is not null, otherwise assign null
        out_TransactionID = out_TransactionItem != null ? DateTime.Now.ToString() : null;
    }
}