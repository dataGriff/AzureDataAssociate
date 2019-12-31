using MongoDB.Driver;
using System;

namespace MyCompanyReader
{
    class Program
    {
        static void Main(string[] args)
        {
            IMongoCollection<Company> companiesCollection;

            MongoClient client = new MongoClient("<YOUR CONNECTION STRING>");

            var database = client.GetDatabase("YOUR DATABASE NAME");

            companiesCollection = database.GetCollection<Company>("<YOUR COLLECTION NAME>");

            var myCompanies = companiesCollection.Find<Company>(company => company.founded_year > 2000).Limit(100).ToList();

            foreach (var company in myCompanies)
            {
                Console.WriteLine($"{company.name} founded in {company.founded_year}.");
            }

            Console.WriteLine("press a key to exit ...");
            Console.ReadKey();
        }
    }
}
