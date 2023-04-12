using System.Data;
using Marionette.Excel_Scope;

namespace Marionette_Framework
{
    partial class Framework
    {
        public void InitAllSettings(string in_ConfigFile, string[] in_ConfigSheets, DataTable in_SettingsDb,
            out Dictionary<string, object> out_Config)
        {
            //Log Message (Initialize All Settings)
            Console.WriteLine("Initializing settings...");
        
            //Assign out_Config (initialization)
            out_Config = new Dictionary<string, object>();

            //For each configuration sheet
            foreach (var Sheet in in_ConfigSheets)
            {
                var excelFile = new Excel(in_ConfigFile);
            
                //Read range (Settings and Constants sheets)
                var dt_SettingsAndConstants = excelFile.ToDataTableModern(Sheet);
                excelFile.Close();

                //For each configuration row
                foreach (DataRow Row in (IEnumerable<DataRow>)dt_SettingsAndConstants)
                {
                    //If configuration row is not empty
                    if (!string.IsNullOrWhiteSpace(Row["Name"].ToString().Trim()))
                    {
                        //Add Config key/value pair
                        out_Config[Row["Name"].ToString().Trim()] = Row["Value"];
                    }
                }

                //Try initializing assets
                try
                {
                    //Get Orchestrator Assets
                    excelFile = new Excel(in_ConfigFile);
                
                    //Read Range (Assets sheet)
                    var dt_Assets = excelFile.ToDataTableModern(Sheet);

                    //For each asset row
                    foreach (DataRow Row in (IEnumerable<DataRow>)dt_Assets)
                    {
                        //Try retrieving asset from Orchestrator
                        try
                        {
                            //Get orchestrator asset
                            var AssetValue = in_SettingsDb.AsEnumerable()
                                .Where(row => row.Field<string>(0) == Row[0].ToString())
                                .Select(row => row.Field<string>(1))
                                .FirstOrDefault();
                        
                            //Assign value in config
                            out_Config[Row["Name"].ToString()] = AssetValue;
                        }
                        catch (Exception e)
                        {
                            //If asset name is specified, but it cannot be retrieved
                            if (!string.IsNullOrWhiteSpace(Row["Name"].ToString().Trim()))
                            {
                                //Throw AssetFailedToLoad Exception
                                throw new Exception("Loading asset " + Row["Asset"] + " failed: " + e.Message);
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
}