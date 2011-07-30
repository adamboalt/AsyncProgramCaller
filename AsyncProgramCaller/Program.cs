using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsyncProgramCaller
{
    // Das hier könnte eine Bibliothek sein
    static class Extensions
    {
        /// <summary>
        /// Gets the ExitCode of a process without blocking. If the process is still running, it will return the code as soon as the process exits.
        /// </summary>
        /// <param name="p">A valid process</param>
        /// <returns>An ExitCode</returns>
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
            p.Start();

            // Prozess Ende
            Console.WriteLine("Exit code was {0}", await p.GetExitCodeAsync());
        }
    }
}
