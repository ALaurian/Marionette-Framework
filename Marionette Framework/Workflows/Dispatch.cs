//This namespace is for the Marionette Framework.

using System.Data;
using Marionette.Excel_Scope;

namespace Marionette_Framework;

//This is a partial class declaration for the Framework class.
static partial class Workflows
{
    //This method Dispatches data from a DT into the Queue using a Dispatcher instance.
    public static void Dispatch(string in_FrameworkSettingsPath)
    {
        var OrchestratorQueueName = Config["OrchestratorQueueName"].ToString();
        
        //Here we add the dispatcherInput
        //Write logic to add the DataTable, either from Excel, a Json file or a text file..
        chromeBrowser.Click("//*[contains(text(),'Download Excel')]");
        var downloadedFiles = chromeBrowser.GetDownloadedFiles();
        var excelFile = new Excel(chromeBrowser.GetDownloadedFilePath(0));

        var dataTable = excelFile.WriteDataTableFromExcel(1);
        excelFile.Close();
        _dispatcherInput = dataTable;

        try
        {
            //Create a new instance of the Dispatcher class with the given parameters.
            var dispatcher = new Dispatcher(
                _dispatcherInput,
                OrchestratorConnection,
                OrchestratorQueueName,
                int.Parse(Config["MaxRetryNumber"].ToString()),
                in_FrameworkSettingsPath);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Dispatcher added no new queue items into {OrchestratorQueueName} queue.");
        }


    }
}