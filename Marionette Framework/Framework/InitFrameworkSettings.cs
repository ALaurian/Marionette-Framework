//This code initializes framework settings by reading JSON file and creating an OrchestratorConnection object
using Marionette.Orchestrator; //importing namespace Marionette.Orchestrator
using Newtonsoft.Json; //importing Newtonsoft.Json namespace

namespace Marionette_Framework //defining a namespace called Marionette_Framework
{
    partial class Framework //defining a class called Framework
    {
        public void InitFrameworkSettings(string in_FrameworkSettingsPath) //defining a public method called InitFrameworkSettings that takes a string argument called in_FrameworkSettingsPath
        {
            string json = File.ReadAllText(in_FrameworkSettingsPath); //reading the contents of the JSON file into a string variable called json

            FrameworkSettings = JsonConvert.DeserializeObject<FrameworkSettings>(json); //deserializing the json string into an object of type FrameworkSettings and assigning it to a FrameworkSettings object called FrameworkSettings

            try //starting a try block
            {
                //creating a new OrchestratorConnection object with data from the JSON file and assigning it to an OrchestratorConnection object called OrchestratorConnection
                OrchestratorConnection = new Orchestrator(
                    FrameworkSettings.Server,
                    FrameworkSettings.DatabaseName,
                    FrameworkSettings.Username,
                    FrameworkSettings.Password
                );
            }
            catch (Exception e) //catching any exceptions thrown during the try block
            {
                Console.WriteLine("Failed to establish connection to Orchestrator."); //printing an error message
                throw; //re-throwing the exception
            }
        }
    }
}