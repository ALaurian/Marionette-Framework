using System.Data;
using Marionette.Orchestrator;
using Marionette.Orchestrator.Exceptions;
using Marionette.WebBrowser;

namespace Marionette_Framework
{
    partial class Framework
    {
        //Handles SystemExceptions
        public Exception SystemException;
        //Handles BusinessRuleExceptions, this should always be in a throw.
        public BusinessRuleException BusinessException;

        //The config dictionary, contains all the settings
        public Dictionary<string, object> Config;

        //The orchestrator connection to the DB, it has a method that lets you fetch any Table from the DB.
        public OrchestratorConnection OrchestratorConnection;


        public int ConsecutiveSystemExceptions = 0;
        
        //The stopper for the robot, you have to write your own logic for this for now.
        public bool ShouldStop = false;
        
        public int TransactionNumber = 0;
        public QueueItem TransactionItem;
        private string TransactionID;
        
        public FrameworkSettings FrameworkSettings;

        public int RetryNumber = 0;
        
        //Dispatcher input DataTable
        public DataTable _dispatcherInput;
        
        //User created Variables
        public MarionetteWebBrowser _webBrowser;

    }
}