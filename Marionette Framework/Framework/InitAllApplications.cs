using System.Data;
using Marionette.Excel_Scope;
using Marionette.WebBrowser;
using Microsoft.Playwright;
using BrowserType = Marionette.WebBrowser.BrowserType;

namespace Marionette_Framework;

partial class Framework
{
    
    public void InitAllApplications()
    {
        Console.WriteLine("Opening applications...");

        //Here we add the dispatcherInput
        //Write logic to add the DataTable, either from Excel, a Json file or a text file..
        var dataTable = new DataTable();
        
        _dispatcherInput = dataTable;
        OrchestratorConnection.ClearQueue(Config["OrchestratorQueueName"].ToString());
        
    }
}