using System.Data;
using Marionette.Orchestrator;
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

        //Retry Get transaction item
        for (int i = 0; i < Int32.Parse(in_Config["RetryNumberGetTransactionItem"].ToString()); i++)
        {
            try
            {
                var orchestratorQueue = OrchestratorConnection.ReceiveData(FrameworkSettings.QueuesTableName)
                    .AsEnumerable()
                    .FirstOrDefault(row => row.Field<string>(0) == in_Config["OrchestratorQueueName"].ToString());

                // Deserialize the JSON into an array of QueueItem objects
                var queueItems = JsonConvert.DeserializeObject<List<QueueItem>>(orchestratorQueue[1].ToString());

                out_TransactionItem = queueItems[TransactionNumber];

                break;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not retrieve transaction item. Exception message: {e.Message}");

                // if this is the last retry attempt, rethrow the exception
                if (i == Int32.Parse(in_Config["RetryNumberGetTransactionItem"].ToString()) - 1)
                {
                    throw;
                }
            }
        }

        if (out_TransactionItem != null)
        {
            out_TransactionID = DateTime.Now.ToString();
        }
        else
        {
            out_TransactionID = null;
        }
    }
}