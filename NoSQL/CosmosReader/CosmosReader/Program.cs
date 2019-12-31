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

            cosmosClient = new CosmosClient("AccountEndpoint=https://plsightcomsossql.documents.azure.com:443/;AccountKey=VT06XcuDdjPthO7YwxwhMKbN8idcyelnKpAlcy925Tys2XEdfsD542cmrWjYIgeDYcPCsLRi5NCH6LxSvNrViA==;");
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