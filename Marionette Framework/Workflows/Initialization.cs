using System.Data;
using System.Windows;
using Marionette.Orchestrator;
using Newtonsoft.Json;

namespace Marionette_Framework;

partial class Framework
{
    public void Initialization()
    {
        SystemException = null;
        var AssetsTable = new DataTable();//OrchestratorConnection.ReceiveData(FrameworkSettings.AssetTableName);
        
        if (Config == null)
        {
            Console.WriteLine(
                $"The primary screen resolution is: {SystemParameters.PrimaryScreenWidth} x {SystemParameters.PrimaryScreenHeight}");

            InitAllSettings(AssetsTable, out Config);

            if (!string.IsNullOrWhiteSpace(in_OrchestratorQueueName))
            {
                Config["OrchestratorQueueName"] = in_OrchestratorQueueName;
            }

            if (!string.IsNullOrWhiteSpace(in_OrchestratorQueueFolder))
            {
                Config["OrchestratorQueueFolder"] = in_OrchestratorQueueFolder;
            }

            KillAllProcesses();

            //Add Log Fields
        }


        if (Int32.Parse(Config["MaxConsecutiveSystemExceptions"].ToString()) > 0 && ConsecutiveSystemExceptions >=
            Int32.Parse(Config["MaxConsecutiveSystemExceptions"].ToString()))
        {
            throw new Exception(Config["ExceptionMessage_ConsecutiveErrors"] +
                                " Consecutive retry number: " + (ConsecutiveSystemExceptions + 1));
        }

        InitAllApplications();
    }
}