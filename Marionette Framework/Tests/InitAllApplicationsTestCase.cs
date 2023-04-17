using NUnit.Framework;

namespace Marionette_Framework.Tests;

public partial class Tests
{
    public void InitAllApplicationsTestCase()
    {
        Console.WriteLine("InitAllApplicationsTestCase started.");
        var framework = new Framework();

        //Initializes settings from the FrameworkSettings.json
        framework.InitFrameworkSettings("Data/FrameworkSettings.json");

        //Initializes settings from the Config.json
        framework.Initialization("Data/Config.json");

        //Verify activity output
        Assert.AreEqual(new object(), new object(), "");

        framework.CloseAllApplications();
    }
}