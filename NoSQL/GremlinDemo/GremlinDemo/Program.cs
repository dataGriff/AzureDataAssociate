using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Exceptions;
using Gremlin.Net.Structure.IO.GraphSON;
using Newtonsoft.Json;

namespace GremlinDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var hostname = "plsightnosqlgremlin.gremlin.cosmos.azure.com";
            var port = 443;
            string authKey = "lYY8g3MvmSpBDl1DrMws529EUqRhykjXPsIHfDPVjXVRnYDe7v5MQdWvdyDyOzrAYYblYPoG6tgU4vkCuy8usw==";
            string database = "graphs";
            string collection = "mygraph";

            var gremlinServer = new GremlinServer(hostname, port, enableSsl: true,
                                        username: "/dbs/" + database + "/colls/" + collection,
                                        password: authKey);

            using (var gremlinClient = new GremlinClient(gremlinServer, new GraphSON2Reader()
                , new GraphSON2Writer(), GremlinClient.GraphSON2MimeType))
            {
                var gremlinQueries = new List<string>
                {
                    "g.V().drop()",

                    "g.addV('person').property('id', 'reza').property('firstName', 'Reza').property('email', 'reza@test.com').property('city', 'Toronto')",

                    "g.addV('person').property('id', 'john').property('firstName', 'John').property('email', 'john@test.com').property('city', 'Boston')",

                    "g.V('john').addE('knows').to(g.V('reza'))",

                    "g.addV('cellphone').property('id', 'androidphone').property('os', 'Android').property('manufacturer', 'LG').property('city', 'Toronto')",

                    "g.V('reza').addE('uses').to(g.V('androidphone'))",
                };

                foreach (var query in gremlinQueries)
                {
                    var resultSet = gremlinClient.SubmitAsync<dynamic>(query).Result;

                    if (resultSet.Count > 0)
                    {
                        Console.WriteLine("Result:");
                        foreach (var result in resultSet)
                        {
                            // The vertex results are formed as Dictionaries with a nested dictionary for their properties
                            string output = JsonConvert.SerializeObject(result);
                            Console.WriteLine($"{output}");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }

        private static void PrintStatusAttributes(IReadOnlyDictionary<string, object> attributes)
        {
            Console.WriteLine($"StatusAttributes:");
            Console.WriteLine($"[\"x-ms-status-code\"] : { GetValueAsString(attributes, "x-ms-status-code")}");
            Console.WriteLine($"[\"x-ms-total-request-charge\"] : { GetValueAsString(attributes, "x-ms-total-request-charge")}");
        }

        public static string GetValueAsString(IReadOnlyDictionary<string, object> dictionary, string key)
        {
            return JsonConvert.SerializeObject(GetValueOrDefault(dictionary, key));
        }

        public static object GetValueOrDefault(IReadOnlyDictionary<string, object> dictionary, string key)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }

            return null;
        }
    }
}
