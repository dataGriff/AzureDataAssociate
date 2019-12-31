using Microsoft.WindowsAzure.Storage.Table;

namespace ConsoleApp17
{
    public class Person : TableEntity
    {
        public Person()
        {
        }

        public Person(string pk, string rk)
        {
            PartitionKey = pk;
            RowKey = rk;
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }        
    }
}