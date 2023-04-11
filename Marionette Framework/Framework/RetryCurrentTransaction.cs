namespace Marionette_Framework;

partial class Framework
{
    private void RetryCurrentTransaction(Dictionary<string, object> in_Config,
        ref int io_RetryNumber,
        ref int io_TransactionNumber,
        Exception in_SystemException,
        bool in_QueueRetry)
    {
        if (Convert.ToInt32(in_Config["MaxRetryNumber"]) > 0)
        {
            if (io_RetryNumber >= Convert.ToInt32(in_Config["MaxRetryNumber"]))
            {
                Console.WriteLine(in_Config["LogMessage_ApplicationException"] +
                                  " Max number of retries reached. " + in_SystemException.Message + " at Source: " +
                                  in_SystemException.Source);
                io_RetryNumber = 0;
                io_TransactionNumber++;
            }
            else
            {
                Console.WriteLine(in_Config["LogMessage_ApplicationException"] + " Retry: " +
                                  io_RetryNumber + ". " + in_SystemException.Message + " at Source: " +
                                  in_SystemException.Source);

                if (in_QueueRetry)
                {
                    io_TransactionNumber++;
                }
                else
                {
                    io_RetryNumber++;
                }
            }
        }
        else
        {
            Console.WriteLine(in_Config["LogMessage_ApplicationException"] + in_SystemException.Message +
                              " at Source: " + in_SystemException.Source);
            io_TransactionNumber++;
        }
    }
}