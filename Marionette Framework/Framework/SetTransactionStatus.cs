using Marionette.Orchestrator;
using Marionette.Orchestrator.Enums;
using Marionette.Orchestrator.Exceptions;

namespace Marionette_Framework;

partial class Framework
{
    public void SetTransactionStatus(BusinessRuleException in_BusinessException,
        Dictionary<string, object> in_Config,
        QueueItem in_TransactionItem,
        ref int io_RetryNumber,
        ref int io_TransactionNumber,
        string in_TransactionID,
        Exception in_SystemException,
        ref int io_ConsecutiveSystemExceptions)
    {
        int state = 0;
        if (in_BusinessException == null && in_SystemException == null)
        {
            Success(in_Config, in_TransactionItem);
            state = 1;
        }
        else if (in_BusinessException != null)
        {
            Business_Exception(in_Config, in_TransactionItem);
            state = 1;
        }
        else if (in_BusinessException == null)
        {
            System_Exception(in_Config, in_TransactionItem, ref io_RetryNumber, ref io_TransactionNumber,
                in_SystemException, ref io_ConsecutiveSystemExceptions);
        }

        switch (state)
        {
            case 1:
                io_TransactionNumber++;
                io_RetryNumber = 0;
                io_ConsecutiveSystemExceptions = 0;
                break;
        }
    }

    private void System_Exception(Dictionary<string, object> in_Config, QueueItem in_TransactionItem,
        ref int io_RetryNumber,
        ref int io_TransactionNumber, Exception in_SystemException, ref int io_ConsecutiveSystemExceptions)
    {
        Console.WriteLine("Consecutive system exception counter is " + io_ConsecutiveSystemExceptions +
                          ".");
        var QueueRetry = in_TransactionItem != null &&
                         in_TransactionItem.GetType().FullName.Equals(Settings.TransactionDataType);

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

    private void Business_Exception(Dictionary<string, object> in_Config, QueueItem in_TransactionItem)
    {
        if (in_TransactionItem != null && in_TransactionItem.GetType().FullName.Equals(Settings.TransactionDataType))
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

    private void Success(Dictionary<string, object> in_Config, QueueItem in_TransactionItem)
    {
        if (in_TransactionItem != null && in_TransactionItem.GetType().FullName.Equals(Settings.TransactionDataType))
        {
            //Retry Get transaction item
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