using Marionette.Orchestrator;

namespace Marionette_Framework;

partial class Framework
{
    public void Process(QueueItem in_TransactionItem, Dictionary<string, object> in_Config)
    {
        Console.WriteLine("Started Process");
        
        //Invoke steps of process.
    }
}