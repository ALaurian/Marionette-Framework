using System.Data;
using Marionette.Excel_Scope;
using Marionette.WebBrowser;
using Microsoft.Playwright;
using BrowserType = Marionette.WebBrowser.BrowserType;

namespace Marionette_Framework;

partial class Framework
{
    
    public static void InitAllApplications(Dictionary<string, object> in_Config, ref MarionetteWebBrowser io_chromeBrowser)
    {
        Console.WriteLine("Opening applications...");

        io_chromeBrowser = new MarionetteWebBrowser(BrowserType.Chrome);
        io_chromeBrowser.Navigate(in_Config["rpaChallengeURL"].ToString(), io_chromeBrowser.GetPageByIndex(0));
    }
}