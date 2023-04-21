using Marionette.WebBrowser;
using BrowserType = Marionette.WebBrowser.BrowserType;

namespace Marionette_Framework;

static partial class Framework
{
    
    public static void InitAllApplications(string url, ref MarionetteWebBrowser io_chromeBrowser)
    {
        Console.WriteLine("Opening applications...");

        if (io_chromeBrowser == null)
        {
            io_chromeBrowser = new MarionetteWebBrowser(BrowserType.Chrome);
            io_chromeBrowser.Navigate(url, io_chromeBrowser.GetPageByIndex(0));
        }
        
    }
}