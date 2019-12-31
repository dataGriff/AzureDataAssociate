using Cassandra;
using Cassandra.Mapping;
using System;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace CassandraClient
{
    class Program
    {
        private const string UserName = "<YOUR USERNAME>";
        private const string Password = "<YOUR PASSWORD>==";
        private const string CassandraContactPoint = "<YOUR ACCOUNT NAME>.cassandra.cosmos.azure.com";   
        private static int CassandraPort = 10350;

        static void Main(string[] args)
        {
            // Connect to cassandra cluster  (Cassandra API on Azure Cosmos DB supports only TLSv1.2)
            var options = new Cassandra.SSLOptions(SslProtocols.Tls12, true, ValidateServerCertificate);
            options.SetHostNameResolver((ipAddress) => CassandraContactPoint);
            Cluster cluster = Cluster.Builder().WithCredentials(UserName, Password)
                .WithPort(CassandraPort).AddContactPoint(CassandraContactPoint)
                .WithSSL(options).Build();
            ISession session = cluster.Connect();

            session = cluster.Connect("keyspace01");

            IMapper mapper = new Mapper(session);

            mapper.Insert<People>(new People(1, "Reza", "reza@test.com"));
            mapper.Insert<People>(new People(2, "John", "john@test.com"));
            mapper.Insert<People>(new People(3, "Mary", "mary@test.com"));
        }

        public static bool ValidateServerCertificate(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);
            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }
    }
}
