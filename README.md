# SQL with Azure Functions
[![Open in GitHub Codespaces](https://github.com/codespaces/badge.svg)](https://codespaces.new/pjirsa/sql-sproc-function)

Examples of working with SQL bindings, triggers, and direct ADO.NET connections in Azure Functions. These samples are based on code located in [https://github.com/Azure/azure-functions-sql-extension](https://github.com/Azure/azure-functions-sql-extension).

## Try It
1. Clone, or fork then clone, this repo to your local machine.
2. Ensure Visual Studio is installed, or use Azure Function Core Tools.
3. Add local.settings.json file then add the following content.
```
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "sql_connection": "<your sql connection string HERE>"
  }
}
```
4. Use scripts found in [Database](./Database) folder to create 'Products' and 'ProductsWithIdentity' tables in your database. Insert some sample items to 'Products' table. 
5. Add the 'SelectProductsCost' stored procedure in order to test the 'CallSproc' function.
6. Start the function app by debuggin in Visual Studio, or from the command line with `func start`.