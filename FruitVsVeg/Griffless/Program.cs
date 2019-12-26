using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Azure.EventHubs;
using System.Text;
using FruitConsole;
using System.Net;
using System.Linq;

namespace Griffless
{
    class Program
    {
        private static EventHubClient eventHubClient;

        static void Main(string[] args)
        {
            string connehub = "Endpoint=sb://grifftestjson.servicebus.windows.net/;SharedAccessKeyName=send;SharedAccessKey=MhPDUtbFl/kP/RN3C30QOdljUlxtkLrf3k+zsSJigDw=";
            string hubname = "fruit";
           // string hubname2 = "fruit";
           // string hubname3 = "veg";

           // int fruitcount = 0;
          //  int vegcount = 0;


            while (1 == 1)
                {
                    //WaitStandard();

                    string a = "F";
                    Fruit fruit = new Fruit(a);
                    var message = JsonConvert.SerializeObject(fruit);                 //Remember Install-Package Newtonsoft.Json
                Console.WriteLine(message);
               // Console.ReadKey();

                EventHubWrapper(connehub, hubname, message).GetAwaiter().GetResult();

                /*
                    //Console.WriteLine(message);
               //     EventHubWrapper(connehub, hubname, message).GetAwaiter().GetResult();
                 //   EventHubWrapper(connehub, hubname2, message).GetAwaiter().GetResult();

                fruitcount++;
                Console.WriteLine("done this many fruit: " + fruitcount.ToString());

                string b = "V";
                    Veg veg = new Veg(b);
                    var message2 = JsonConvert.SerializeObject(veg);                 //Remember Install-Package Newtonsoft.Json
                                                                                     //Console.WriteLine(message2);

            //    EventHubWrapper(connehub, hubname, message2).GetAwaiter().GetResult();
               // EventHubWrapper(connehub2, hubname3, message2).GetAwaiter().GetResult();
           //    EventHubWrapper(connehub, hubname, message2).GetAwaiter().GetResult(); 
             //   EventHubWrapper(connehub, hubname3, message2).GetAwaiter().GetResult(); 

                vegcount++;
                Console.WriteLine("done this many veg: "+ vegcount.ToString());

                Console.WriteLine("done batch");
                */

            }
        }

        public static void RepeatAction(int repeatCount, Action action)
        {
            for (int i = 0; i < repeatCount; i++)
                action();
        }

        public static void WaitStandard()
        {
            int standardWait =  500;
            System.Threading.Thread.Sleep(standardWait);
        }

        private static async Task EventHubWrapper(string connectionString, string hubName, string message)
        {
            var connectionStringBuilder = new EventHubsConnectionStringBuilder(connectionString)    //Install-Package Microsoft.Azure.EventHubs
            {
                EntityPath = hubName
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
