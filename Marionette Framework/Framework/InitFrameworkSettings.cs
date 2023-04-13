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
        
    }
}