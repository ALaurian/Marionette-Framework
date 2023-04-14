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
        //Write logic to add the DataTable
        _dispatcherInput = new DataTable();
    }
}