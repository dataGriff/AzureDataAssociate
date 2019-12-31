using System;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;

namespace CosmosReader
{
    class Program
    {
        static void Main(string[] args)
        {
            CosmosClient cosmosClient;
            Database database;
            Container container;

            cosmosClient = new CosmosClient("AccountEndpoint=https://sqlapidemoreza.documents.azure.com:443/;AccountKey=B3y6ygB9fYFAf4zHhN9paOyc70JlTYY2BmmIcUZT364iqCgkgdSWcSeSAvHqaO5vgRFQ6LlzjQvpzdxkDxauCA==;");
            database = cosmosClient.GetDatabase("destinationdb01");
            container = database.GetContainer("Products");

            var sqlQueryText = "SELECT * FROM c WHERE c.Color = 'Red'";
            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<Product> queryResultSetIterator = container.GetItemQueryIterator<Product>(queryDefinition);

            List<Product> redProducts = new List<Product>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Product> currentResultSet = queryResultSetIterator.ReadNextAsync().Result;
                foreach (Product product in currentResultSet)
                {
                    redProducts.Add(product);
                    Console.WriteLine("\tRead {0}\n", product.ProductNumber);
                }
            }

        }
    }
}