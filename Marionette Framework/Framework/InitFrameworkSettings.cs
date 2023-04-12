using Newtonsoft.Json;

namespace Marionette_Framework;

partial class Framework
{
    public void InitFrameworkSettings(Framework framework)
    {
        // Read the JSON file into a string
        string json = File.ReadAllText("Data/Settings.json");

        // Deserialize the JSON string into an OrchestratorConfig object
        framework.Settings = JsonConvert.DeserializeObject<Settings>(json);
    }
}