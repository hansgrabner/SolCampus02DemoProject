namespace DemoTableProject
{
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;
    using System.Collections.Concurrent;

    //https://learn.microsoft.com/en-us/azure/storage/tables/table-storage-overview

    class Product : TableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double Price { get; set; }

        public bool Status { get; set; }
        public DateTime Date { get; set; }

        public Product(int id, string name, DateTime date, double price, bool status)
        {
            Name = name;
            Date = date;
            Price = price;
            Status = status;
            PartitionKey = "Products";
            RowKey = id.ToString();
        }

        public Product() : this(1, "", new DateTime(), 0, true)
        {

        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=");

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("Products");
            var created = table.CreateIfNotExistsAsync().Result;


            DMLOperations(table);


            Console.ReadKey();
        }


        public static async void DMLOperations(CloudTable table)
        {
            await CreateProduct(table, new Product(1, "Prod A", DateTime.Now, 20.5, true));
            await CreateProduct(table, new Product(2, "Prod B", DateTime.Now.AddMinutes(10), 7, false));
            await CreateProduct(table, new Product(3, "Prod C", DateTime.Now.AddDays(-1), 3.9, true));
            await CreateProduct(table, new Product(4, "Prod D", DateTime.Now.AddMonths(-2), 10, false));


            await UpdateProduct(table, "Products", "2", "Test");

            await DisplayProduct(table, "Products", "2");
            await DisplayAllProducts(table);
        }

        static async Task<TableResult> CreateProduct(CloudTable table, Product product)
        {
            TableOperation insert = TableOperation.Insert(product);

            var result = await table.ExecuteAsync(insert);
            return result;

        }

        static async Task<TableQuerySegment<Product>> DisplayAllProducts(CloudTable table)
        {

            TableQuery<Product> query = new TableQuery<Product>()
                    .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Products"));

            var queryRestul = await table.ExecuteQuerySegmentedAsync(query, null);

            Console.WriteLine("DisplayAllProducts begin" + queryRestul.Count());
            foreach (Product product in queryRestul)
            {
                Console.WriteLine(product.Id);
                Console.WriteLine(product.Name);
                Console.WriteLine(product.Price);
            }
            Console.WriteLine("DisplayAllProducts ends");
            return queryRestul;
        }

        static async Task<TableResult> DisplayProduct(CloudTable table, string partitionKey, string rowKey)
        {
            TableOperation retrieve = TableOperation.Retrieve<Product>(partitionKey, rowKey);

            TableResult result = await table.ExecuteAsync(retrieve);

            Console.WriteLine(((Product)result.Result).Name);

            return result;
        }

        static async Task<TableResult> UpdateProduct(CloudTable table, string partitionKey, string rowKey, string newName)
        {
            TableOperation retrieve = TableOperation.Retrieve<Product>(partitionKey, rowKey);

            TableResult result = await table.ExecuteAsync(retrieve);

            Product product = (Product)result.Result;

            product.ETag = "*";
            product.Name = newName;

            if (result != null)
            {
                TableOperation update = TableOperation.Replace(product);

                result = await table.ExecuteAsync(update);
            }
            return result;
        }

    }
}