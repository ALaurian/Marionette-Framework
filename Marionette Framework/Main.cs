using Newtonsoft.Json;

namespace Marionette_Framework;

class Program
{
    static void Main(string[] args)
    {
        var framework = new Framework();
        
        //Initializes settings from the Settings.json
        framework.InitFrameworkSettings();

        Initialization:
        framework.Initialization(framework.Orchestrator(framework.Settings.AssetTableName));

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
                Console.Write(
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
    }


}