using System.Data;
using System.Windows;
using Marionette.Orchestrator;
using Newtonsoft.Json;

namespace Marionette_Framework;

partial class Framework
{
    public DataTable Orchestrator(string tableName, string in_SettingsFile = "Data/Settings.json")
    {
        // Read the JSON file into a string
        string json = File.ReadAllText(in_SettingsFile);

        // Deserialize the JSON string into an OrchestratorConfig object
        Settings = JsonConvert.DeserializeObject<Settings>(json);

        // Create a new OrchestratorConnection object with the data from the JSON file
        OrchestratorConnection = new OrchestratorConnection(
            Settings.DatabaseName,
            Settings.Username,
            Settings.Password
        );

        //Extract table to DataTable
        var settings_DB = OrchestratorConnection.ReceiveData($"{tableName}");

        return settings_DB;
    }
    public void Initialization(DataTable settings_DB)
    {
        SystemException = null;

        if (Config == null)
        {
            Console.WriteLine(
                $"The primary screen resolution is: {SystemParameters.PrimaryScreenWidth} x {SystemParameters.PrimaryScreenHeight}");

            InitAllSettings("Data\\Config.xlsx", new[] { "Settings", "Constants" }, settings_DB, out Config);

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