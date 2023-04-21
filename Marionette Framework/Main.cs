using System.Data;
using System.Windows.Controls;
using System.Xml.Linq;
using FlaUI.Core;
using Marionette.Excel_Scope;
using Marionette.Orchestrator;
using Marionette.WebBrowser;
using Marionette.WinEngine;
using Newtonsoft.Json;
using static Marionette_Framework.Framework;
using static Marionette_Framework.Workflows;

namespace Marionette_Framework;

class Program
{
    private static bool dispatched = false;
    
    static void Main(string[] args)
    {
        //Initializes settings from the FrameworkSettings.json
        InitFrameworkSettings("Data/FrameworkSettings.json");
        
        Initialization:
        //Initializes settings from the Config.json
        Initialization("Data/Config.json");

        //Dispatcher
        if (dispatched == false)
        {
            OrchestratorConnection.ClearQueue(Config["OrchestratorQueueName"].ToString());
            dispatched = Dispatch();
        }
        
        if (Workflows.SystemException == null)
        {
            GetTransactionData:
            GetTransactionData();

            if (TransactionItem == null)
            {
                Console.WriteLine("Process finished due to no more transaction data");
                EndProcess();
            }
            else
            {
                Console.WriteLine(
                    Config["LogMessage_GetTransactionData"] + TransactionNumber.ToString());

                ProcessTransaction();

                if (Workflows.SystemException == null && BusinessException == null)
                {
                    goto GetTransactionData;
                }

                if (Workflows.SystemException != null)
                {
                    goto Initialization;
                }

                if (BusinessException != null)
                {
                    goto GetTransactionData;
                }
            }
        }
        else
        {
            Console.WriteLine("System exception at initialization: " + Workflows.SystemException.Message +
                              " at Source: " + Workflows.SystemException.Source);
            CloseAllApplications(chromeBrowser);
        }


        
        //Closes the Orchestrator connection
        OrchestratorConnection.CloseConnection();
    }
}