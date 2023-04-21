using static Marionette_Framework.Framework;

namespace Marionette_Framework;

partial class Workflows
{
    public static void GetTransactionData()
    {
        //check stop signal
        if (ShouldStop)
        {
            Console.WriteLine("Stop process requested.");
            TransactionItem = null;
        }
        else
        {
            GetTransaction(TransactionNumber, Config, out TransactionItem, out TransactionID, OrchestratorConnection);
        }
    }
}