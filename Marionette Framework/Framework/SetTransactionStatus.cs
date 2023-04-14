using Marionette.Orchestrator;
using Marionette.Orchestrator.Enums;
using Marionette.Orchestrator.Exceptions;
using Newtonsoft.Json;

namespace Marionette_Framework;

partial class Framework
{
//This is a public method named SetTransactionStatus that takes in several parameters by reference
    public void SetTransactionStatus(BusinessRuleException in_BusinessException,
        Dictionary<string, object> in_Config,
        QueueItem in_TransactionItem,
        ref int io_RetryNumber,
        ref int io_TransactionNumber,
        string in_TransactionID,
        Exception in_SystemException,
        ref int io_ConsecutiveSystemExceptions)
    {
        int state = 0; //initialize a variable named state to 0

        //check if both in_BusinessException and in_SystemException are null
        if (in_BusinessException == null && in_SystemException == null)
        {
            Success(in_Config,
                ref in_TransactionItem); //call Success method and pass in_Config and in_TransactionItem by reference
            state = 1; //set the state to 1
        }
        //check if in_BusinessException is not null
        else if (in_BusinessException != null)
        {
            Business_Exception(in_Config,
                ref in_TransactionItem); //call Business_Exception method and pass in_Config and in_TransactionItem by reference
            state = 1; //set the state to 1
        }
        //check if in_BusinessException is null
        else if (in_BusinessException == null)
        {
            //call System_Exception method and pass in_Config, in_TransactionItem, io_RetryNumber, io_TransactionNumber, in_SystemException and io_ConsecutiveSystemExceptions by reference
            System_Exception(in_Config, ref in_TransactionItem, ref io_RetryNumber, ref io_TransactionNumber,
                in_SystemException, ref io_ConsecutiveSystemExceptions);
        }

        //check the state variable using switch statement
        switch (state)
        {
            //if state is 1, increment io_TransactionNumber by 1, set io_RetryNumber and io_ConsecutiveSystemExceptions to 0
            case 1:
                io_TransactionNumber++;
                io_RetryNumber = 0;
                io_ConsecutiveSystemExceptions = 0;
                break;
        }
    }


    private void System_Exception(Dictionary<string, object> in_Config, ref QueueItem in_TransactionItem,
        ref int io_RetryNumber,
        ref int io_TransactionNumber, Exception in_SystemException, ref int io_ConsecutiveSystemExceptions)
    {
        Console.WriteLine("Consecutive system exception counter is " + io_ConsecutiveSystemExceptions +
                          ".");
        var QueueRetry = in_TransactionItem != null;

        try
        {
            //take screenshot
            //in_Folder = in_Config["ExScreenshotsFolderPath"].ToString()
            //io_FilePath = ScreenshotPath
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to take screenshot: " + e.Message + " at Source: " +
                              e.Source + " At Source: " + e.Source);
        }

        if (QueueRetry)
        {
            for (int i = 0; i < Int32.Parse(in_Config["RetryNumberSetTransactionStatus"].ToString()); i++)
            {
                try
                {
                    in_TransactionItem.Status = QueueItemStatus.Failed;
                    io_RetryNumber = in_TransactionItem.RetryNo;
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Could not set the transaction status. " + e.Message);
                    throw;
                }
            }
        }

        io_ConsecutiveSystemExceptions++;

        RetryCurrentTransaction(in_Config, ref io_RetryNumber, ref io_TransactionNumber, in_SystemException,
            QueueRetry);

        try
        {
            CloseAllApplications();
        }
        catch (Exception e)
        {
            Console.WriteLine("CloseAllApplications failed. " + e.Message + " at Source: " + e.Source);
            try
            {
                KillAllProcesses();
            }
            catch (Exception exception)
            {
                Console.WriteLine("KillAllProcesses failed. " + e.Message + " at Source: " + e.Source);
            }
        }
    }

    private void Business_Exception(Dictionary<string, object> in_Config, ref QueueItem in_TransactionItem)
    {
        if (in_TransactionItem != null)
        {
            //Retry Get transaction item
            for (int i = 0; i < Int32.Parse(in_Config["RetryNumberSetTransactionStatus"].ToString()); i++)
            {
                try
                {
                    in_TransactionItem.Status = QueueItemStatus.Failed;
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Could not set the transaction status. " + e.Message + " At Source: " + e.Source);
                    throw;
                }
            }
        }

        Console.WriteLine(in_Config["LogMessage_Success"].ToString());
    }

    private void Success(Dictionary<string, object> in_Config, ref QueueItem in_TransactionItem)
    {
        if (in_TransactionItem != null)
        {
            for (int i = 0; i < Int32.Parse(in_Config["RetryNumberSetTransactionStatus"].ToString()); i++)
            {
                try
                {
                    in_TransactionItem.Status = QueueItemStatus.Successful;
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Could not set the transaction status. " + e.Message + " At Source: " + e.Source);
                    throw;
                }
            }
        }

        Console.WriteLine(in_Config["LogMessage_Success"].ToString());
    }
    
}