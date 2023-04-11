namespace Marionette_Framework;

partial class Framework
{
    public void GetTransactionData()
    {
        //check stop signal
        if (ShouldStop)
        {
            Console.WriteLine("Stop process requested.");
            TransactionItem = null;
        }
        else
        {
            GetTransaction(TransactionNumber, Config, out TransactionItem, out TransactionID);
        }
    }
}