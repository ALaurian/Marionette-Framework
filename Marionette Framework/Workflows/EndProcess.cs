using System.Diagnostics;

namespace Marionette_Framework;

partial class Framework
{
    public void EndProcess()
    {
        try
        {
            CloseAllApplications();
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