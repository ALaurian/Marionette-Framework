using NUnit.Framework;
using static Marionette_Framework.Framework;

namespace Marionette_Framework.Tests;

public partial class Tests
{
    public static void InitAllApplicationsTestCase()
    {
        Console.WriteLine("InitAllApplicationsTestCase started.");

        //Initializes settings from the FrameworkSettings.json
        InitFrameworkSettings("Data/FrameworkSettings.json");

        //Initializes settings from the Config.json
        Workflows.Initialization("Data/Config.json");

        //Verify activity output
        Assert.AreEqual(new object(), new object(), "");

        //CloseAllApplications();
    }
}