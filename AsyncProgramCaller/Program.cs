using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AsyncProgramCaller
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessStartInfo info = new ProcessStartInfo()
            {
                FileName = "Sleeper.exe",
                Arguments = "5000",
                WorkingDirectory = Environment.CurrentDirectory
            };
            var p = Process.Start(info);
            var started = DateTime.Now;
            p.WaitForExit();
            Console.WriteLine("Exit code was {0}", p.ExitCode);
            Console.WriteLine("I waited for {0}", DateTime.Now - started);
            Console.ReadKey();
        }
    }
}
