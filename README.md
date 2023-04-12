## Description of the Marionette Framework

The UiPath ReFramework is a template designed to automate business processes using a standard framework that provides modularity, scalability, and extensibility. The template is designed to manage the entire lifecycle of a transaction, from initialization to processing to closing, and to handle exceptions and retries.

The C#.NET port of the UiPath ReFramework follows the same structure and design principles as the original template, with some modifications to adapt it to a C# code environment.

### Data folder

The Data folder contains the main configuration files and classes used by the framework.

- `Config.xlsx`: This is the main configuration file for the framework, which contains the settings and parameters for the workflow.

- `Orchestrator.cs`: This is a class that handles the connection to the UiPath Orchestrator and returns a DataTable containing the queue data.

- `QueueItemTemplate.json`: This is a template file for creating an SQL database to implement the queue item object.

- `Settings.cs`: This is a class that loads the sensitive data from the `Settings.json` file.

- `Settings.json`: This file contains the sensitive data, such as the login credentials for the database and the transactionDataType.

- `Variables.cs`: This file contains all the global variables used by the framework.

### Framework folder

The Framework folder contains the core classes and activities that define the UiPath ReFramework template.

- `CloseAllApplications.cs`: This activity closes all the running applications at the end of a transaction.

- `GetTransaction.cs`: This activity retrieves the next transaction item from the queue.

- `InitAllApplications.cs`: This activity initializes all the required applications at the start of a transaction.

- `InitAllSettings.cs`: This activity initializes all the settings at the start of a transaction.

- `InitFrameworkSettings.cs`: This activity initializes the `Settings` variable from the `Settings.json` file.

- `KillAllProcesses.cs`: This activity kills all the running processes at the end of a transaction.

- `Process.cs`: This activity processes the current transaction item.

- `RetryCurrentTransaction.cs`: This activity retries the current transaction in case of an exception.

- `SetTransactionStatus.cs`: This activity sets the status of the transaction item in the queue.

### Workflows folder

The Workflows folder contains the main workflows that use the framework.

- `EndProcess.cs`: This workflow is called at the end of the process and performs the final cleanup.

- `GetTransactionData.cs`: This workflow retrieves the next transaction item from the queue and initializes the required settings and applications.

- `Initialization.cs`: This workflow initializes the `Settings` variable and performs the required setup.

- `ProcessTransaction.cs`: This workflow is responsible for performing the necessary actions to complete the transaction, such as retrieving data, performing calculations, and updating records. It leverages the features of the ReFramework, such as transaction logging, exception handling, and retry mechanisms, to ensure the successful completion of the transaction.

### Main.cs

## Description of the Main function in the UiPath ReFramework ported to C#.NET

The `Main` function is the entry point of the application and contains the main workflow of the UiPath ReFramework. The purpose of the function is to retrieve transaction data from a queue, process the data, and update the status of the transaction in the queue.

The `Main` function starts by creating a new instance of the `Framework` class and initializing the settings from the `Settings.json` file using the `InitFrameworkSettings` method.

The function then enters a loop labeled `Initialization`, which calls the `Initialization` workflow. The workflow initializes the required settings and applications for the process.

If there are no system exceptions during the initialization, the function enters another loop labeled `GetTransactionData`. This loop retrieves the next transaction item from the queue using the `GetTransactionData` workflow and initializes the required settings and applications.

If there are no more transaction items in the queue, the `EndProcess` workflow is called to perform the final cleanup and the function exits.

If a transaction item is retrieved from the queue, the `ProcessTransaction` workflow is called to process the transaction. If there are no exceptions during the processing of the transaction, the function goes back to the `GetTransactionData` loop to retrieve the next transaction item.

If a system exception occurs during the processing of a transaction, the `Initialization` workflow is called to reinitialize the settings and applications. The function then goes back to the `GetTransactionData` loop to retrieve the next transaction item.

If a business exception occurs during the processing of a transaction, the function goes back to the `GetTransactionData` loop to retrieve the next transaction item.

If a system exception occurs during the initialization of the framework, the function displays an error message and calls the `CloseAllApplications` method to close all running applications.

In summary, the `Main` function provides a modular and scalable workflow for processing transaction data from a queue using the power of C# code.

### Remarks

The use of goto statements in the code is not a recommended coding practice as it can make the code difficult to understand and maintain. However, in this particular case, the goto statements were used to replicate a state machine.
The UiPath ReFramework is based on a state machine design pattern, which involves transitioning between different states based on input and output events. The use of goto statements was a deliberate choice to mimic this state machine design pattern in C# code.

## How to get started with the Marionette Framework

To get started with the Marionette Framework, follow these steps:

1. Clone or download the C#.NET port of the UiPath ReFramework from the repository.

2. Open the solution file in your preferred IDE.

3. In the `Data` folder, edit the `Config.xlsx` file to set the required configuration settings for your workflow.

4. In the `Data` folder, edit the `Settings.json` file to set the required sensitive data, such as the login credentials for the database and the transaction data type.

5. In the `QueueItemTemplate.json` file, define the structure of your SQL database to implement the queue item object.

6. Implement your workflow by editing the `Process` workflow in the `Framework` folder and adding new workflows in the `Workflows` folder.

7. Build and run your workflow by starting the `Main` function in the `Main.cs` file.

8. Test and debug your workflow, and iterate on it as necessary.


