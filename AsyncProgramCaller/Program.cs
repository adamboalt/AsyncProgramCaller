using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace AsyncProgramCaller
{
    static class Extensions
    {
        public static Task<int> GetExitCodeAsync(this Process p)
        {
            // Create the task to be returned
            var tcs = new TaskCompletionSource<int>(p);

            // Setup the callback event handlers
            p.EnableRaisingEvents = true;
            p.Exited += (s, e) =>
            {
                Process handle = (s as Process);
                tcs.TrySetResult(handle.ExitCode);
            };

            // Return the task that represents the async operation
            return tcs.Task;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Main() darf nicht "async" laufen
            // Run() wird sofort zurückkehren
            Run();            
            // Hindere Programm am beenden
            Console.WriteLine("Ich führe 'Run()' später weiter. Wartet nicht auf mich!");
            Console.ReadKey();
        }

        private static async void Run()
        {
            // Prozess vorbereiten
            ProcessStartInfo info = new ProcessStartInfo()
            {
                FileName = "Sleeper.exe", // Ein Programm, dass nichts tut
                Arguments = "6000", // Laufzeit: 6 Sekunden
                WorkingDirectory = Environment.CurrentDirectory
            };
            var p = new Process();
            p.StartInfo = info;

            // Prozess Start (asynchron)
            var started = DateTime.Now;
            p.Start();

            // beginn (warten)
            for(int i = 0; i < 2; i++)
            {
                Console.WriteLine(i);
                //Thread.Sleep(1000);
            }
            // ende (warten)

            // Prozess Ende
            Console.WriteLine("Exit code was {0}", await p.GetExitCodeAsync());
            Console.WriteLine("I waited for {0}", p.ExitTime - started);            
        }
    }
}
