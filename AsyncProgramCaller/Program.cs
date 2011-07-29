using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncProgramCaller
{
    class Program
    {
        // Flag: Prozess beendet
        static bool done = false;

        static void Main(string[] args)
        {
            // Prozess vorbereiten
            ProcessStartInfo info = new ProcessStartInfo()
            {
                FileName = "Sleeper.exe", // Ein Programm, dass nichts tut
                Arguments = "3000", // Laufzeit: 3 Sekunden
                WorkingDirectory = Environment.CurrentDirectory
            };
            var p = new Process();
            p.StartInfo = info;
            // Signal-Hilfsfunktion
            p.Exited += new EventHandler((s, e) => {
                done = true;
            });
            p.EnableRaisingEvents = true;

            // Prozess Start
            var started = DateTime.Now;            
            p.Start();

            // beginn (warten)
            int i = 0;
            while (!done)
            {
                Console.WriteLine(++i);
                p.WaitForExit(1000);
            }
            // ende (warten)

            // Prozess Ende
            Console.WriteLine("Exit code was {0}", p.ExitCode);
            Console.WriteLine("I waited for {0}", p.ExitTime - started);
            Console.ReadKey();
        }
    }
}
