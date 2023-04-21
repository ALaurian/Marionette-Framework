using System.Windows;
using static Marionette_Framework.Framework;

namespace Marionette_Framework;

static partial class Workflows
{
    public static void Initialization(string in_ConfigPath)
    {
        
        SystemException = null;
        var AssetsTable = OrchestratorConnection.ReceiveData(Framework_Settings.AssetTableName);

        if (Config == null)
        {
            Console.WriteLine(
                $"The primary screen resolution is: {SystemParameters.PrimaryScreenWidth} x {SystemParameters.PrimaryScreenHeight}");

            InitAllSettings(in_ConfigPath, AssetsTable, out Config);

            KillAllProcesses();

            //Add Log Fields
        }


        if (Int32.Parse(Config["MaxConsecutiveSystemExceptions"].ToString()) > 0 &&
            Int32.Parse(Config["ConsecutiveSystemExceptions"].ToString()) >=
            Int32.Parse(Config["MaxConsecutiveSystemExceptions"].ToString()))
        {
            throw new Exception(Config["ExceptionMessage_ConsecutiveErrors"] +
                                " Consecutive retry number: " +
                                (Int32.Parse(Config["ConsecutiveSystemExceptions"].ToString()) + 1));
        }

        InitAllApplications(Config["rpaChallengeURL"].ToString(), ref chromeBrowser);
    }
}