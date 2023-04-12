using Newtonsoft.Json;

namespace Marionette_Framework;

partial class Framework
{
    public void InitFrameworkSettings()
    {
        // Read the JSON file into a string
        string json = File.ReadAllText("Data/FrameworkSettings.json");

        // Deserialize the JSON string into an OrchestratorConfig object
        FrameworkSettings = JsonConvert.DeserializeObject<FrameworkSettings>(json);
        
    }
}