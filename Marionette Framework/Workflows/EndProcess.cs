using System.Diagnostics;
using static Marionette_Framework.Framework;

namespace Marionette_Framework;

static partial class Workflows
{
    public static void EndProcess()
    {
        try
        {
            CloseAllApplications(chromeBrowser);
        }
        catch (Exception e)
        {
            Console.WriteLine("Applications failed to close gracefully. " + e.Message + " at Source: " + e.Source);
            KillAllProcesses();
        }
        finally
        {
            if (SystemException != null && Convert.ToBoolean(Config["ShouldMarkJobAsFaulted"].ToString()))
            {
                // Get the current process object
                Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();

                // Kill the current process
                currentProcess.Kill();
            }
        }
    }
}