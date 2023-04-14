using System.Data;
using Marionette.Excel_Scope;
using Marionette.WebBrowser;
using Newtonsoft.Json;

namespace Marionette_Framework;

class Program
{
    private static bool dispatched = false;
    static void Main(string[] args)
    {
        var framework = new Framework();

        //Initializes settings from the FrameworkSettings.json
        framework.InitFrameworkSettings("Data/FrameworkSettings.json");

        Initialization:
        //Initializes settings from the Config.json
        framework.Initialization("Data/Config.json");

        //Dispatcher
        if (dispatched == false)
        {
            framework.Dispatch("Data/FrameworkSettings.json");
            dispatched = true;
        }
        
        
        if (framework.SystemException == null)
        {
            GetTransactionData:
            framework.GetTransactionData();

            if (framework.TransactionItem == null)
            {
                Console.WriteLine("Process finished due to no more transaction data");
                framework.EndProcess();
            }
            else
            {
                Console.WriteLine(
                    framework.Config["LogMessage_GetTransactionData"] + framework.TransactionNumber.ToString());

                framework.ProcessTransaction();

                if (framework.SystemException == null && framework.BusinessException == null)
                {
                    goto GetTransactionData;
                }

                if (framework.SystemException != null)
                {
                    goto Initialization;
                }

                if (framework.BusinessException != null)
                {
                    goto GetTransactionData;
                }
            }
        }
        else
        {
            Console.WriteLine("System exception at initialization: " + framework.SystemException.Message +
                              " at Source: " + framework.SystemException.Source);
            framework.CloseAllApplications();
        }


        //Closes the Orchestrator connection
        framework.OrchestratorConnection.CloseConnection();
    }
}