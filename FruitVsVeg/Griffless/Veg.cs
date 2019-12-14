using FruitConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Griffless
{
    public class Veg
    {
        public string colour { get; set; }

        public string name { get; private set; }

        public int price { get; private set; }

        public string a { get; private set; }

        public string b { get; private set; }

        public int c { get; private set; }

        public StandardLog standardLog { get; private set; }

        public string EventType;

        public Veg(string eventType)

        {
            this.EventType = eventType;
            this.colour = "Yellow";
            GetFruitNameAndPrice();
            standardLog = new StandardLog(StandardLog.LogCode.Success);
            a = "a";
            b = "b";
            c = 1;
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
