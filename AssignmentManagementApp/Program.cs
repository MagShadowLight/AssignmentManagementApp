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
            var services = new ServiceCollection();
            SingletonCollection.AddMultipleServices(services);
            //services.AddSingleton<AssignmentController>();
            var serviceProvider = services.BuildServiceProvider();
            var consoleUI = serviceProvider.GetRequiredService<ConsoleUI>();
            consoleUI.Run();
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }

    public class SingletonCollection
    {
        public SingletonCollection() { }

        public static void AddMultipleServices(ServiceCollection collection)
        {
            collection.AddSingleton<IAssignmentService, AssignmentService>();
            collection.AddSingleton<IAppLogger, ConsoleAppLogger>();
            collection.AddSingleton<IAssignmentFormatter, AssignmentFormatter>();
            collection.AddSingleton<ConsoleUI>();
        } 
    }
}
