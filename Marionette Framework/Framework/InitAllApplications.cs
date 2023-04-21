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

        chromeBrowser = new MarionetteWebBrowser(BrowserType.Chrome);
        chromeBrowser.Navigate(Config["rpaChallengeURL"].ToString(), chromeBrowser.GetPageByIndex(0));
    }
}