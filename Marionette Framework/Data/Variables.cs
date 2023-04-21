using System.Data;
using Marionette.Orchestrator;
using Marionette.Orchestrator.Exceptions;
using Marionette.WebBrowser;

namespace Marionette_Framework
{
    static partial class Workflows
    {
        //Handles SystemExceptions
        public static Exception SystemException;
        //Handles BusinessRuleExceptions, this should always be in a throw.
        public static BusinessRuleException BusinessException;

        //The config dictionary, contains all the settings
        public static Dictionary<string, object> Config;

        //The orchestrator connection to the DB, it has a method that lets you fetch any Table from the DB.
        public static Orchestrator OrchestratorConnection;


        public static int ConsecutiveSystemExceptions = 0;
        
        //The stopper for the robot, you have to write your own logic for this for now.
        public static bool ShouldStop = false;
        
        public static int TransactionNumber = 0;
        public static QueueItem TransactionItem;
        private static string TransactionID;
        
        public static FrameworkSettings Framework_Settings;

        public static int RetryNumber = 0;
        
        //Dispatcher input DataTable
        public static DataTable _dispatcherInput;
        
        //WebBrowser
        public static MarionetteWebBrowser chromeBrowser;

    }
}