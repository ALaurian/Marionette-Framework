using Marionette.Orchestrator.Exceptions;
using static Marionette_Framework.Framework;

namespace Marionette_Framework;

partial class Workflows
{
    public static void ProcessTransaction()
    {
        try
        {
            BusinessException = null;
            Process(ref TransactionItem, Config, chromeBrowser);
            try
            {
                SetTransactionStatus(BusinessException,
                    Config,
                    TransactionItem, ref RetryNumber, ref TransactionNumber,
                    TransactionID,
                    SystemException, ref ConsecutiveSystemExceptions);
            }
            catch (Exception exception)
            {
                Console.WriteLine("SetTransactionStatus.xaml failed: " + exception.Message + " at Source: " +
                                  exception.Source);
            }
        }
        catch (BusinessRuleException ProcessTransactionException)
        {
            BusinessException = ProcessTransactionException;

            try
            {
                SetTransactionStatus(BusinessException,
                    Config,
                    TransactionItem, ref RetryNumber, ref TransactionNumber,
                    TransactionID,
                    SystemException, ref ConsecutiveSystemExceptions);
            }
            catch (Exception exception)
            {
                Console.WriteLine("SetTransactionStatus.xaml failed: " + exception.Message + " at Source: " +
                                  exception.Source);
            }
        }
        catch (Exception Exception)
        {
            SystemException = Exception;

            try
            {
                SetTransactionStatus(BusinessException,
                    Config,
                    TransactionItem, ref RetryNumber, ref TransactionNumber,
                    TransactionID,
                    SystemException, ref ConsecutiveSystemExceptions);
            }
            catch (Exception exception)
            {
                Console.WriteLine("SetTransactionStatus.xaml failed: " + exception.Message + " at Source: " +
                                  exception.Source);
            }
        }
        
        TransactionItem.LastProcessingOn = DateTime.Now.ToString();
        TransactionItem.Progress = "Finished.";
    }
}