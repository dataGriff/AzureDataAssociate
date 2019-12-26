using FruitConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Griffless
{
    public class Fruit
    {
        public string colour { get; set; }

        public string name { get; private set; }

        public int price { get; private set; }

        public StandardLog standardLog { get; private set; }

        public string EventType;

        public Dictionary<string, string> testjson;

        public Fruit(string eventType)

        {
            this.EventType = eventType;
            this.colour = "Yellow";
            GetFruitNameAndPrice();
            standardLog = new StandardLog(StandardLog.LogCode.Success);
            this.testjson = new Dictionary<string, string>();
            testjson.Add("a", "1");
            testjson.Add("b", "2");
            testjson.Add("c", "3");
        }

        public void GetFruitNameAndPrice ()
        {
          switch (colour)
            {
                case "Yellow":
                    name = "Banana";
                    price = 50;
                    break;
            }
        }
    }
}
