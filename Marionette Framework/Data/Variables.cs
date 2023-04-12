using Marionette.Orchestrator;
using Marionette.Orchestrator.Exceptions;

namespace Marionette_Framework
{
    partial class Framework
    {
        public Exception SystemException;
        public BusinessRuleException BusinessException;
        
        public Dictionary<string, object> Config;
        
        public String in_OrchestratorQueueName;
        public string in_OrchestratorQueueFolder;
        
        public int ConsecutiveSystemExceptions;
        
        public OrchestratorConnection OrchestratorConnection;
        
        public bool ShouldStop = false;
        
        public int TransactionNumber = 0;
        public QueueItem TransactionItem;
        private string TransactionID;
        
        public Settings Settings;

        public int RetryNumber = 0;

    }
}