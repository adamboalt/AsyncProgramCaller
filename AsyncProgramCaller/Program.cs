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
        static void Main(string[] args)
        {
            // 'Run()' beinhaltet unsere 'Programmlogik'
            Run();
            // Hindere Programm am Beenden
            Console.WriteLine("Ich bin fertig mit 'Run()' und kann eigentlich beendet werden.");
            Console.ReadKey();
        }

        private static void Run()
        {
            // Prozess vorbereiten
            ProcessStartInfo info = new ProcessStartInfo()
            {
                FileName = "Sleeper.exe", // Ein Programm, dass nichts tut
                Arguments = "6000", // Laufzeit: 3 Sekunden
                WorkingDirectory = Environment.CurrentDirectory
            };
            var p = new Process();
            p.StartInfo = info;

            // Prozess Start (asynchron)
            p.Start();

            // beginn (warten)
            
            // Wir blockieren hier den kompletten Prozess!
            p.WaitForExit();

            // ende (warten)

            // Prozess Ende
            Console.WriteLine("Exit code was {0}", p.ExitCode);
        }
    }
}
