using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sleeper
{
    class Program
    {
        static int Main(string[] args)
        {
            int duration;
            
            if (args.Length > 0)
            {
                if (!Int32.TryParse(args[0], out duration))
                {
                    Console.WriteLine("Could not read integer.");
                    Help();
                    return -2;
                }
                else
                {
                    Console.WriteLine("Good night…");
                    System.Threading.Thread.Sleep(duration);
                    Console.WriteLine("Hello!");
                    return 0;
                }
            }
            else
            {
                Help();
                return -1;
            }
        }

        private static void Help()
        {
            Console.WriteLine("Sleeper.exe does nothing for an amount of time\nUsage: Sleeper.exe <duration>\n\t<duration>\t duration of sleep in milliseconds");
        }
    }
}
