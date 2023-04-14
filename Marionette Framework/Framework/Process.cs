using Marionette.Orchestrator;
using Marionette.Orchestrator.Enums;
using Marionette.WebBrowser;

namespace Marionette_Framework;

partial class Framework
{
    private bool startButtonPressed = false;

    public void Process(ref QueueItem in_TransactionItem, Dictionary<string, object> in_Config)
    {
        Console.WriteLine("Started Process");
        in_TransactionItem.Status = QueueItemStatus.InProgress;
        in_TransactionItem.StartTransactionTime = DateTime.Now.ToString();
        //Invoke steps of processs
    }
}