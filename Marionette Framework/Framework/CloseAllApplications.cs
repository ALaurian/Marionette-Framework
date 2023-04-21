using Marionette.WebBrowser;

namespace Marionette_Framework;

static partial class Framework
{
    public static void CloseAllApplications(MarionetteWebBrowser chromeBrowser)
    {
        Console.WriteLine("Closing applications...");

        chromeBrowser.Close();
    }
}