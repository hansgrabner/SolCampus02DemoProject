using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using System.Collections.Concurrent;

namespace AzureTableDemo
{
    //https://learn.microsoft.com/en-us/dotnet/api/overview/azure/data.tables-readme?view=azure-dotnet
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            string conntectionString = "DefaultEndpointsProtocol=https;AccountName=";

            var serviceClient = new TableServiceClient(conntectionString);

            var tableName = "MyProducts";
            TableItem table = serviceClient.CreateTableIfNotExists(tableName);
            Console.WriteLine($"The created table's name is {table.Name}.");

            var tableClient = new TableClient(conntectionString, tableName);

            var partitionKey = "A";
            var tableEntity = new TableEntity(partitionKey, Guid.NewGuid().ToString())
                {
                    { "Product", "PC" },
                    { "Price", 550.00 },
                    { "Quantity", 21 }
                };

            Console.WriteLine($"{tableEntity.RowKey}: {tableEntity["Product"]} costs ${tableEntity.GetDouble("Price")}.");

            tableClient.AddEntity(tableEntity);

            Pageable<TableEntity> queryResultsFilter = tableClient.Query<TableEntity>(filter: $"PartitionKey eq '{partitionKey}'");

            // Iterate the <see cref="Pageable"> to access all queried entities.
            foreach (TableEntity qEntity in queryResultsFilter)
            {
                Console.WriteLine($"{qEntity.GetString("Product")}: {qEntity.GetDouble("Price")}");
            }

            Console.WriteLine($"The query returned {queryResultsFilter.Count()} entities.");
        }
    }
}