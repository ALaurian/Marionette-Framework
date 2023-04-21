// This code defines a C# class named 'Dispatcher'
namespace Marionette_Framework;

// Importing necessary namespaces
using System.Data;
using Marionette.Orchestrator;
using Marionette.Orchestrator.Enums;

// Defining 'Dispatcher' class
public class Dispatcher
{
    // Constructor with parameters
    public Dispatcher(DataTable in_InputDT, Orchestrator in_OrchestratorConnection, string in_QueueName,
        int in_RetryNo)
    {
        // Check if the specified queue exists in the Orchestrator, if not, create one.
        if (!in_OrchestratorConnection.TableExists(in_QueueName))
        {
            in_OrchestratorConnection.CreateQueue(in_QueueName);
        }

        try
        {
            // Loop through each row in the input DataTable and add it as a new queue item in the Orchestrator queue
            foreach (DataRow row in in_InputDT.Rows)
            {
                // Create a new dictionary to store the content of the current row
                var specificContent = new Dictionary<string, object>();

                // Loop through each column in the current row and add it to the dictionary with column name as key
                foreach (DataColumn column in in_InputDT.Columns)
                {
                    specificContent.Add(column.ColumnName, row[column]);
                }

                // Create a new QueueItem object using the extracted row data and add it to the specified queue
                var QueueItem = new QueueItem(Environment.UserName, "N/A",
                    "N/A", 0, Guid.NewGuid(), "N/A", new Dictionary<string, object>(),
                    QueueItemPriority.Medium, "N/A", in_QueueName, "N/A", in_RetryNo,
                    "N/A", specificContent, "N/A", QueueItemStatus.New, in_OrchestratorConnection);


                in_OrchestratorConnection.AddToQueue(QueueItem, in_QueueName);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"The dispatcher input is empty. No items have been added to the {in_QueueName} queue.");
        }
    }
}