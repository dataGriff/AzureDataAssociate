using System;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using System.Threading.Tasks;
using System.Text;

namespace EventHubReader
{
    class Program
    {


        private const string EventHubConnectionString = "Endpoint=sb://grifffruit.servicebus.windows.net/;SharedAccessKeyName=listen;SharedAccessKey=7HpK49KrK3FF/Lxjof4joKG9kPqbOf3MBi4UxWUBLno=;EntityPath=fruit";
        private const string EventHubName = "fruit";
        private const string StorageContainerName = "hubreader";
        private const string StorageAccountName = "griffhubreader";
        private const string StorageAccountKey = "5B7bgIKMc8SuQFfqf0nJEwP15DWyeoB6MAjytviUB2ok42dLcfbFVBTGZj3/g60R1XII5IagJQ3U7cxMTg852w==";

        private static readonly string StorageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", StorageAccountName, StorageAccountKey);

 


        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            Console.WriteLine("Registering EventProcessor...");

            var eventProcessorHost = new EventProcessorHost(
                EventHubName,
                PartitionReceiver.DefaultConsumerGroupName,
                EventHubConnectionString,
                StorageConnectionString,
                StorageContainerName);

            // Registers the Event Processor Host and starts receiving messages
            await eventProcessorHost.RegisterEventProcessorAsync<SimpleEventProcessor>();

            Console.WriteLine("Receiving. Press ENTER to stop worker.");
            Console.ReadLine();

            // Disposes of the Event Processor Host
            await eventProcessorHost.UnregisterEventProcessorAsync();
        }


    }
}
