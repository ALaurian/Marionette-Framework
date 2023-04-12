using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Marionette_Framework
{
    partial class Framework
    {
        public void InitAllSettings(DataTable in_AssetsTable,
            out Dictionary<string, object> out_Config, out Dictionary<string, object> out_Assets)
        {
            //Log Message (Initialize All FrameworkSettings)
            Console.WriteLine("Initializing settings...");

            // Load the JSON file as a string
            string jsonString = File.ReadAllText("Data/Config.json");

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
                if (assetsArray != null)
                {
                    // Create a new Dictionary<string, object> to hold the assets
                    // Iterate over the objects in the array and extract the key-value pairs
                    out_Assets =
                        (from asset in assetsArray.OfType<JObject>()
                            select asset.Properties().FirstOrDefault()
                            into keyProperty
                            where keyProperty != null
                            select keyProperty.Value.ToString())
                        .ToDictionary<string, string, object>(key => key, key => null);
                }
                else
                {
                    out_Assets = new Dictionary<string, object>();
                }

                // Create a new dictionary to hold out_Assets
                var dictPlaceholder = out_Assets;

                //For each asset row
                foreach (var asset in dictPlaceholder)
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
                        out_Assets[asset.Key] = AssetValue;
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
                throw e;
            }
        }
    }
}