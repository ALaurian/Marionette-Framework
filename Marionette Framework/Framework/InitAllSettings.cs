// Import necessary libraries
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Marionette_Framework
{
    // Define the Framework class
    static partial class Framework
    {
        // Define a public method for initializing all framework settings
        public static void InitAllSettings(string in_ConfigPath, DataTable in_AssetsTable, out Dictionary<string, object> out_Config)
        {
            //Log message to console indicating that settings initialization has started
            Console.WriteLine("Initializing settings...");
            
            // Load the JSON file as a string
            string jsonString = File.ReadAllText(in_ConfigPath);

            // Deserialize the JSON into a JObject
            JObject configJObject = JsonConvert.DeserializeObject<JObject>(jsonString);

            // Convert the remaining properties to a Dictionary<string, object>
            out_Config = configJObject.ToObject<Dictionary<string, object>>();
            out_Config.Remove("Assets");

            //Try initializing assets
            try
            {
                // Get the "Assets" property from the JObject
                JArray assetsArray = configJObject.GetValue("Assets") as JArray;
                Dictionary<string,object> assets;
                if (assetsArray != null)
                {
                    // Create a new Dictionary<string, object> to hold the assets
                    // Iterate over the objects in the array and extract the key-value pairs
                    assets =
                        (from asset in assetsArray.OfType<JObject>()
                            select asset.Properties().FirstOrDefault()
                            into keyProperty
                            where keyProperty != null
                            select keyProperty.Value.ToString())
                        .ToDictionary<string, string, object>(key => key, key => null);
                }
                else
                {
                    assets = new Dictionary<string, object>();
                }
                
                //Concatenate Dictionaries
                out_Config = out_Config
                    .Concat(assets)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                
                //For each asset row
                foreach (var asset in assets)
                {
                    //Try retrieving asset from Orchestrator
                    try
                    {
                        //Get orchestrator asset
                        var AssetValue = in_AssetsTable.AsEnumerable()
                            .Where(row => row.Field<string>(0) == asset.Key)
                            .Select(row => row.Field<object>(1))
                            .FirstOrDefault();

                        //Assign value in config
                        out_Config[asset.Key] = AssetValue;
                    }
                    catch (Exception e)
                    {
                        //If asset name is specified, but it cannot be retrieved
                        if (!string.IsNullOrWhiteSpace(asset.ToString()))
                        {
                            //Throw AssetFailedToLoad Exception
                            throw new Exception("Loading asset " + asset + " failed: " + e.Message);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //Rethrow loading asset exception
                throw;
            }
        }
    }
} 
