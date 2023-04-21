using System.Windows.Controls;
using static Marionette_Framework.Workflows;

namespace Marionette_Framework.Tests;

public partial class Tests
{
    public static void GetTransactionDataTestCase()
    {
        Console.WriteLine("GetTransactionDataTestCase started.");
        var framework = new Framework();

        //Initializes settings from the FrameworkSettings.json
        Framework.InitFrameworkSettings("Data/FrameworkSettings.json");

        //Initializes settings from the Config.json
        Initialization("Data/Config.json");

        //Dispatcher
        if (dispatched == false)
        {
            Dispatch("Data/FrameworkSettings.json");
            dispatched = true;
        }

        GetTransactionData();
        var VerificationOutput = TransactionItem != null;

        if (VerificationOutput)
        {
            TransactionItem.Progress = "Tested";
        }
    }
}