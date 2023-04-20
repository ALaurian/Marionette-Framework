//This namespace is for the Marionette Framework.

using System.Data;

namespace Marionette_Framework;

//This is a partial class declaration for the Framework class.
partial class Framework
{
    //This method Dispatches data from a DT into the Queue using a Dispatcher instance.
    public void Dispatch(string in_FrameworkSettingsPath)
    {
        var OrchestratorQueueName = Config["OrchestratorQueueName"].ToString();
        //Here we add the dispatcherInput
        //Write logic to add the DataTable, either from Excel, a Json file or a text file..
        var dataTable = new DataTable();
        _dispatcherInput = dataTable;

        try
        {
            //Create a new instance of the Dispatcher class with the given parameters.
            var dispatcher = new Dispatcher(
                _dispatcherInput,
                OrchestratorConnection,
                OrchestratorQueueName,
                int.Parse(Config["MaxRetryNumber"].ToString()),
                in_FrameworkSettingsPath);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Dispatcher added no new queue items into {OrchestratorQueueName} queue.");
        }


    }
}