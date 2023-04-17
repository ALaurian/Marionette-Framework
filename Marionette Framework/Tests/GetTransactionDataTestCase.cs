namespace Marionette_Framework.Tests;

public partial class Tests
{
    public void GetTransactionDataTestCase()
    {
        Console.WriteLine("GetTransactionDataTestCase started.");
        var framework = new Framework();

        //Initializes settings from the FrameworkSettings.json
        framework.InitFrameworkSettings("Data/FrameworkSettings.json");

        //Initializes settings from the Config.json
        framework.Initialization("Data/Config.json");

        //Dispatcher
        if (dispatched == false)
        {
            framework.Dispatch("Data/FrameworkSettings.json");
            dispatched = true;
        }

        framework.GetTransactionData();
        var VerificationOutput = framework.TransactionItem != null;

        if (VerificationOutput)
        {
            framework.TransactionItem.Progress = "Tested";
        }
    }
}