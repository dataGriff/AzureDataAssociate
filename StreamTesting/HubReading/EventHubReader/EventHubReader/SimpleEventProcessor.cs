using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using System.Threading.Tasks;

namespace EventHubReader
{
    public class SimpleEventProcessor : IEventProcessor
    {
        private static EventHubClient eventHubClient;

        public Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            Console.WriteLine($"Processor Shutting Down. Partition '{context.PartitionId}', Reason: '{reason}'.");
            return Task.CompletedTask;
        }

        public Task OpenAsync(PartitionContext context)
        {
            Console.WriteLine($"SimpleEventProcessor initialized. Partition: '{context.PartitionId}'");
            return Task.CompletedTask;
        }

        public Task ProcessErrorAsync(PartitionContext context, Exception error)
        {
            Console.WriteLine($"Error on Partition: {context.PartitionId}, Error: {error.Message}");
            return Task.CompletedTask;
        }

        public Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {
            foreach (var eventData in messages)
            {
                var data = Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);
                Console.WriteLine($"Message received. Partition: '{context.PartitionId}', Data: '{data}'");
                try
                {
                    EventHubWrapper(data).GetAwaiter().GetResult();
                }
                catch
                {
                    Console.WriteLine("whatever");
                }
            }

            return context.CheckpointAsync();
        }

        private static async Task EventHubWrapper(string message)
        {
            string connfruitehub = "Endpoint=sb://griffruit2.servicebus.windows.net/;SharedAccessKeyName=send;SharedAccessKey=Onun3wbEgjXBIp6szK3szvoOHdKS3biotJ2uShZQMjI=;EntityPath=fruit3";
            string fruitehubname = "fruit3";

            var connectionStringBuilder = new EventHubsConnectionStringBuilder(connfruitehub)    //Install-Package Microsoft.Azure.EventHubs
            {
                EntityPath = fruitehubname
            };

            eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());

            await SendMessageToEventHub(message);

            await eventHubClient.CloseAsync();
        }

        private static async Task SendMessageToEventHub(string message)
        {
            await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));
        }
    }
}
