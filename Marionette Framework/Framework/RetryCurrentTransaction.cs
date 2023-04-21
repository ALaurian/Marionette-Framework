namespace Marionette_Framework; 

static partial class Framework 
{
    private static void RetryCurrentTransaction(Dictionary<string, object> in_Config,
        ref int io_RetryNumber,
        ref int io_TransactionNumber,
        Exception in_SystemException,
        bool in_QueueRetry) // Declares a private method with the specified parameters.
    {
        if (Convert.ToInt32(in_Config["MaxRetryNumber"]) > 0) // If the maximum number of retries specified in the input configuration is greater than zero.
        {
            if (io_RetryNumber >= Convert.ToInt32(in_Config["MaxRetryNumber"])) // If the current retry number is greater than or equal to the maximum number of retries.
            {
                Console.WriteLine(in_Config["LogMessage_ApplicationException"] + // Writes the specified error message to the console.
                                  " Max number of retries reached. " + in_SystemException.Message + " at Source: " +
                                  in_SystemException.Source);
                io_RetryNumber = 0; // Resets the current retry number to zero.
                io_TransactionNumber++; // Increments the transaction number.
            }
            else // If the current retry number is less than the maximum number of retries.
            {
                Console.WriteLine(in_Config["LogMessage_ApplicationException"] + " Retry: " +
                                  io_RetryNumber + ". " + in_SystemException.Message + " at Source: " +
                                  in_SystemException.Source); // Writes the specified retry error message to the console.

                if (in_QueueRetry) // If the in_QueueRetry parameter is true.
                {
                    io_TransactionNumber++; // Increments the transaction number.
                }
                else // If the in_QueueRetry parameter is false.
                {
                    io_RetryNumber++; // Increments the current retry number.
                }
            }
        }
        else // If the maximum number of retries specified in the input configuration is zero or less.
        {
            Console.WriteLine(in_Config["LogMessage_ApplicationException"] + in_SystemException.Message +
                              " at Source: " + in_SystemException.Source); // Writes the specified error message to the console.
            io_TransactionNumber++; // Increments the transaction number.
        }
    }
}
