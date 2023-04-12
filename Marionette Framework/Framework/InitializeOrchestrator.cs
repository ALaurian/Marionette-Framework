using System.Data;
using Marionette.Orchestrator;

namespace Marionette_Framework;

partial class Framework
{
    public void InitializeOrchestrator()
    {
        try
        {
            // Create a new OrchestratorConnection object with the data from the JSON file
            OrchestratorConnection = new OrchestratorConnection(
                FrameworkSettings.Server,
                FrameworkSettings.DatabaseName,
                FrameworkSettings.Username,
                FrameworkSettings.Password
            );
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed to establish connection to Orchestrator.");
        }

    }
}