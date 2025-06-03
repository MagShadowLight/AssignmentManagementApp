using AssignmentManagementApp.Api.Controllers;
using AssignmentManagementApp.Core;
using AssignmentManagementApp.Core.Interfaces;
using AssignmentManagementApp.UI;
using Microsoft.Extensions.DependencyInjection;

namespace AssignmentManagementApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Starting Application");
            //ConsoleColors.OtherColor();
            //Console.WriteLine("Creating Services");
            var services = new ServiceCollection();

            
            services.AddSingleton<IAssignmentService,AssignmentService>();
            services.AddSingleton<IAppLogger, ConsoleAppLogger>();
            services.AddSingleton<IAssignmentFormatter, AssignmentFormatter>();
            services.AddSingleton<ConsoleUI>();
            //services.AddSingleton<AssignmentController>();

            var serviceProvider = services.BuildServiceProvider();
            var consoleUI = serviceProvider.GetRequiredService<ConsoleUI>();
            //Console.WriteLine("Services Created");

            //Console.WriteLine("Opening UI");
            consoleUI.Run();
            //Console.WriteLine("UI Closed");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
