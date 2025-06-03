using AssignmentManagementApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagementApp.Core
{
    public class ConsoleAppLogger : IAppLogger
    {
        public void Error(string message)
        {
            Console.WriteLine($"[ERROR] {message}");
        }

        public void Log(string message)
        {
            Console.WriteLine($"[LOG] {message}");
        }

        public void Warn(string message)
        {
            Console.WriteLine($"[WARNING] {message}");
        }
    }
}
