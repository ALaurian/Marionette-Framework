using System.Data;
using FlaUI.Core.Tools;
using Marionette.Orchestrator;
using Marionette.Orchestrator.Enums;
using Newtonsoft.Json;

namespace Marionette_Framework;

partial class Framework
{
    private void GetTransaction(int in_TransactionNumber, Dictionary<string, object> in_Config,
        out QueueItem out_TransactionItem, out string out_TransactionID)
    {
        //Log Message Get Transaction Item
        Console.WriteLine("Get the transaction item.");
        out_TransactionItem = null;

        bool retry = true;
        int retries = 0;

        var orchestratorQueue = OrchestratorConnection.ReceiveData(FrameworkSettings.QueuesTableName)
            .AsEnumerable()
            .FirstOrDefault(row => row.Field<string>(0) == in_Config["OrchestratorQueueName"].ToString());

        // Deserialize the JSON into an array of QueueItem objects
        var queueItems = JsonConvert.DeserializeObject<List<QueueItem>>(orchestratorQueue[1].ToString());

        while (retry)
        {
            if (in_TransactionNumber == queueItems.Count)
                break;
            
            try
            {
                if (queueItems[in_TransactionNumber].Status == QueueItemStatus.New)
                {
                    out_TransactionItem = queueItems[in_TransactionNumber];
                    break;
                }

                if (queueItems[in_TransactionNumber].Status != QueueItemStatus.New)
                {
                    in_TransactionNumber++;
                }
                
            }
            catch (Exception e)
            {
                retry = true;
                Console.WriteLine($"Could not retrieve transaction item. Exception message: {e.Message}");

                // if this is the last retry attempt, rethrow the exception
                if (retries == Int32.Parse(in_Config["RetryNumberGetTransactionItem"].ToString()) - 1)
                {
                    throw;
                }
            }
        }

        out_TransactionID = out_TransactionItem != null ? DateTime.Now.ToString() : null;
    }
}