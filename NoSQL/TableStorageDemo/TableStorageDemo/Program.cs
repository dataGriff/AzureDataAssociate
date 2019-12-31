using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace ConsoleApp17
{
    class Program
    {
        static void Main(string[] args)
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=plsightnosqltable;AccountKey=k7CqVE9aj4sIH8vI9wTJCOTo5bMe/2qjwUMnuO+j8nJd1cvAWkPj4qstCYoDVDyM8ECO7YoqtsTsN2m3PUbtUA==;EndpointSuffix=core.windows.net";

            CloudStorageAccount account = CloudStorageAccount.Parse(storageConnectionString);

            CloudTableClient serviceClient = account.CreateCloudTableClient();

            CloudTable table = serviceClient.GetTableReference("people");

            var opperation = TableOperation.Retrieve<Person>("canada", "1");

            var jobResult = table.ExecuteAsync(opperation);

            var entity = jobResult.Result;

        }
    }
}