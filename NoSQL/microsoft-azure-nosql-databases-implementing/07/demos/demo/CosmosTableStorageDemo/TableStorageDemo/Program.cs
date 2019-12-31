using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace ConsoleApp17
{
    class Program
    {
        static void Main(string[] args)
        {
            string storageConnectionString = "<your storage account connection string>";

            CloudStorageAccount account = CloudStorageAccount.Parse(storageConnectionString);

            CloudTableClient serviceClient = account.CreateCloudTableClient();

            CloudTable table = serviceClient.GetTableReference("<your table name>");

            var opperation = TableOperation.Retrieve<Person>("<partition key>", "<row key>");

            var jobResult = table.ExecuteAsync(opperation);

            var entity = jobResult.Result;

        }
    }
}