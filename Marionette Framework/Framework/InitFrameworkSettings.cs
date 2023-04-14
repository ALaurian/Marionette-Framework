using Marionette.Orchestrator;
using Newtonsoft.Json;

namespace Marionette_Framework;

partial class Framework
{
    public void InitFrameworkSettings(string in_FrameworkSettingsPath)
    {
        // Read the JSON file into a string
        string json = File.ReadAllText(in_FrameworkSettingsPath);

        // Deserialize the JSON string into an OrchestratorConfig object
        FrameworkSettings = JsonConvert.DeserializeObject<FrameworkSettings>(json);
        
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
            throw;
        }


    }
}