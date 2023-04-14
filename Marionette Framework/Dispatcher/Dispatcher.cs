using System.Data;
using Marionette.Orchestrator;
using Marionette.Orchestrator.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Marionette_Framework;

public class Dispatcher
{
    public Dispatcher(DataTable in_InputDT, OrchestratorConnection in_OrchestratorConnection, string in_QueueName, int in_RetryNo, string in_FrameworkSettingsPath)
    {
        // Read the JSON file into a string
        var json = File.ReadAllText(in_FrameworkSettingsPath);

        // Deserialize the JSON string into an OrchestratorConfig object
        var FrameworkSettings = JsonConvert.DeserializeObject<FrameworkSettings>(json);
        
        var queueItems = new List<QueueItem>();
        int id = 0;

        // get the existing queue items for the queue name

        var existingQueueItemsJson = in_OrchestratorConnection.GetJsonFromQueuesTable(in_QueueName);

        if (!string.IsNullOrEmpty(existingQueueItemsJson))
        {
            // deserialize the existing queue items into a list
            var existingQueueItems = JsonConvert.DeserializeObject<List<QueueItem>>(existingQueueItemsJson);

            // find the max id in the existing queue items
            var maxId = existingQueueItems.Max(x => x.Id);

            // start from the next id
            id = maxId + 1;

            // add the new QueueItems to the existing list
            foreach (DataRow row in in_InputDT.Rows)
            {
                var queueItem = new QueueItem
                {
                    // set the properties of the QueueItem from the values in the current row
                    AssignedTo = "",
                    DeferDate = System.DateTime.Now,
                    DueDate = System.DateTime.Now,
                    Id = id,
                    ItemKey = Guid.NewGuid(),
                    LastProcessingOn = "",
                    Output = new Dictionary<string, object>(),
                    Priority = QueueItemPriority.Medium,
                    Progress = "Started",
                    QueueName = in_QueueName,
                    Reference = "",
                    RetryNo = in_RetryNo,
                    ReviewStatus = "",
                    SpecificContent = new Dictionary<string, object>(),
                    StartTransactionTime = DateTime.Now,
                    Status = QueueItemStatus.New
                };

                // convert the ItemArray of the current row to a Dictionary and add it to SpecificContent
                Dictionary<string, object> specificContent = new Dictionary<string, object>();
                for (var i = 0; i < row.ItemArray.Length; i++)
                {
                    specificContent.Add(in_InputDT.Columns[i].ColumnName, row[i]);
                }

                queueItem.SpecificContent = specificContent;

                existingQueueItems.Add(queueItem);
                id++;
            }

            // serialize the updated list of QueueItems
            string queueItemsJson =
                JsonConvert.SerializeObject(existingQueueItems, Formatting.Indented, new StringEnumConverter());

            // update the existing JSON value in the SQL table
            in_OrchestratorConnection.UpdateJsonInQueuesTable(in_QueueName, queueItemsJson);

            in_OrchestratorConnection.CloseConnection();
        }
        else
        {
            // create new QueueItems from the DataTable and add them to a new list
            foreach (DataRow row in in_InputDT.Rows)
            {
                var queueItem = new QueueItem
                {
                    // set the properties of the QueueItem from the values in the current row
                    AssignedTo = "",
                    DeferDate = System.DateTime.Now,
                    DueDate = System.DateTime.Now,
                    Id = id,
                    ItemKey = Guid.NewGuid(),
                    LastProcessingOn = "",
                    Output = new Dictionary<string, object>(),
                    Priority = QueueItemPriority.Medium,
                    Progress = "Started",
                    QueueName = in_QueueName,
                    Reference = "",
                    RetryNo = in_RetryNo,
                    ReviewStatus = "",
                    SpecificContent = new Dictionary<string, object>(),
                    StartTransactionTime = DateTime.Now,
                    Status = QueueItemStatus.New
                };

                // convert the ItemArray of the current row to a Dictionary and add it to SpecificContent
                Dictionary<string, object> specificContent = new Dictionary<string, object>();
                for (var i = 0; i < row.ItemArray.Length; i++)
                {
                    specificContent.Add(in_InputDT.Columns[i].ColumnName, row[i]);
                }

                queueItem.SpecificContent = specificContent;

                queueItems.Add(queueItem);
                id++;
            }

            // serialize the new list of QueueItems
            string queueItemsJson =
                JsonConvert.SerializeObject(queueItems, Formatting.Indented, new StringEnumConverter());

            // insert the new JSON value into the SQL table
            in_OrchestratorConnection.UpdateJsonInQueuesTable(in_QueueName, queueItemsJson);
            
        }
    }
}