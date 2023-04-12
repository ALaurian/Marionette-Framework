using System.Data;
using Marionette.Orchestrator;

namespace Marionette_Framework;

partial class Framework
{
    public void InitializeOrchestrator()
    {
        // Create a new OrchestratorConnection object with the data from the JSON file
        OrchestratorConnection = new OrchestratorConnection(
            Settings.Server,
            Settings.DatabaseName,
            Settings.Username,
            Settings.Password
        );
    }
}