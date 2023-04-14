namespace Marionette_Framework;

partial class Framework
{
    public void Dispatch(string in_FrameworkSettingsPath)
    {
        //The dispatcher class, it sends all the data from a DT into the Queue.
        var dispatcher = new Dispatcher(
            _dispatcherInput,
            OrchestratorConnection,
            Config["OrchestratorQueueName"].ToString(),
            int.Parse(Config["MaxRetryNumber"].ToString()),
            in_FrameworkSettingsPath);
    }

}