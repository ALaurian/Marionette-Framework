using System.Data;
using Marionette.Orchestrator;

namespace Marionette_Framework;

partial class Framework
{
    public DataTable Orchestrator(string tableName)
    {
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
}