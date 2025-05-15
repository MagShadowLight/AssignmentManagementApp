using AssignmentManagementApp.Core;
using AssignmentManagementApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagementApp.UI
{
    public class ConsoleUI
    {
        private readonly IAssignmentService _assignmentService;
        private readonly IAppLogger _logger;
        private readonly IAssignmentFormatter _formatter;

        public ConsoleUI(IAssignmentService assignmentService, IAppLogger logger, IAssignmentFormatter formatter)
        {
            _assignmentService = assignmentService;
            _logger = logger;
            _formatter = formatter;
        }

        public void Run()
        {
            //Console.WriteLine("UI opened");
            while (true)
            {
                ConsoleColors.MainUIColor();
                Console.WriteLine("Assignment Manager Menu:");
                Console.WriteLine("1. Add Assignment");
                Console.WriteLine("2. List All Assignments");
                Console.WriteLine("3. List Incomplete Assignments");
                Console.WriteLine("4. Mark Assignment as Complete");
                Console.WriteLine("5. Search Assignment by Title");
                Console.WriteLine("6. Update Assignment");
                Console.WriteLine("7. Delete Assignment");
                Console.WriteLine("0. Exit");
                ConsoleColors.InputColor();
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        ConsoleColors.MainUIColor();
                        AddAssignment();
                        break;
                    case "2":
                        ConsoleColors.MainUIColor();
                        ListAllAssignments();
                        break;
                    case "3":
                        ConsoleColors.MainUIColor();
                        ListIncompleteAssignments();
                        break;
                    case "4":
                        ConsoleColors.MainUIColor();
                        MarkAssignmentComplete();
                        break;
                    case "5":
                        ConsoleColors.MainUIColor();
                        SearchAssignmentByTitle();
                        break;
                    case "6":
                        ConsoleColors.MainUIColor();
                        UpdateAssignment();
                        break;
                    case "7":
                        ConsoleColors.MainUIColor();
                        DeleteAssignment();
                        break;
                    case "0":
                        ConsoleColors.OtherColor();
                        Console.WriteLine("Goodbye");
                        //Console.WriteLine("UI Closing");
                        return;
                    default:
                        ConsoleColors.ErrorColor();
                        Console.WriteLine("Invalid choice. Try again");
                        break;
                }
            }
        }

        public void AddAssignment()
        {
            Console.Write("Enter assignment title: ");
            ConsoleColors.InputColor();
            var title = Console.ReadLine();
            ConsoleColors.MainUIColor();
            Console.Write("Enter assignment description: ");
            ConsoleColors.InputColor();
            var description = Console.ReadLine();
            ConsoleColors.MainUIColor();
            try
            {
                var a = new Assignment(title, description);
                if (_assignmentService.AddAssignment(a))
                {
                    ConsoleColors.SuccessColor();
                    Console.WriteLine("Assignment added");
                }
                else
                {
                    ConsoleColors.ErrorColor();
                    Console.WriteLine("An assignment with this title already exists.");
                }
            }
            catch (ArgumentException ex)
            {
                ConsoleColors.ErrorColor();
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void ListAllAssignments()
        {
            var assignments = _assignmentService.ListAll();
            if (assignments.Count == 0)
            {
                Console.WriteLine("No assignments found.");
                return;
            }

            foreach (var assignment in assignments)
            {
                Console.WriteLine($"- {assignment.Title}: {assignment.Description} (Completed: {assignment.IsComplete})");
            }
        }

        private void ListIncompleteAssignments()
        {
            var assignments = _assignmentService.ListIncomplete();
            if (assignments.Count == 0)
            {
                Console.WriteLine("No incomplete assignments found.");
                return;
            }

            foreach(var assignment in assignments)
            {
                Console.WriteLine($"- {assignment.Title}: {assignment.Description} (Completed: {assignment.IsComplete})");
            }
        }

        private void MarkAssignmentComplete()
        {
            Console.Write("Enter the title of the assignment to mark complete: ");
            ConsoleColors.InputColor();
            var title = Console.ReadLine();
            ConsoleColors.MainUIColor();
            if (_assignmentService.MarkAssignmentComplete(title))
            {
                ConsoleColors.SuccessColor();
                Console.WriteLine("Assignment marked as complete.");
            } else
            {
                ConsoleColors.ErrorColor();
                Console.WriteLine("Error: Assignment not found.");
                
            }
        }

        private void SearchAssignmentByTitle()
        {
            Console.Write("Enter the title to search: ");
            ConsoleColors.InputColor();
            var title = Console.ReadLine();
            ConsoleColors.MainUIColor();
            var assignment = _assignmentService.FindAssignmentByTitle(title);

            if (assignment != null)
            {
                Console.WriteLine($"Found: {assignment.Title}: {assignment.Description} (Completed: {assignment.IsComplete})");
            } else
            {
                ConsoleColors.ErrorColor();
                Console.WriteLine("Error: Assignment not found.");
            }
        }

        private void UpdateAssignment()
        {
            Console.Write("Enter the current title of assignment to update: ");
            ConsoleColors.InputColor();
            var oldTitle = Console.ReadLine();
            ConsoleColors.MainUIColor();
            Console.Write("Enter the new title: ");
            ConsoleColors.InputColor();
            var newTitle = Console.ReadLine();
            ConsoleColors.MainUIColor();
            Console.Write("Enter the new description: ");
            ConsoleColors.InputColor();
            var newDescription = Console.ReadLine();

            try
            {
                if (_assignmentService.UpdateAssignment(oldTitle, newTitle, newDescription))
                {
                    ConsoleColors.SuccessColor();
                    Console.WriteLine("Assignment updated successfully.");
                }
                else
                {
                    ConsoleColors.ErrorColor();
                    Console.WriteLine("Error: Assignment update failed. Title may conflict or assignment not found.");
                }
            } catch (Exception ex)
            {
                ConsoleColors.ErrorColor();
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void DeleteAssignment()
        {
            Console.Write("Enter the assignment title to delete:");
            ConsoleColors.InputColor();
            var title = Console.ReadLine();
            ConsoleColors.MainUIColor();
            if (_assignmentService.DeleteAssignment(title))
            {
                ConsoleColors.SuccessColor();
                Console.WriteLine("Assignment deleted successfully.");
            }
            else
            {
                ConsoleColors.ErrorColor();
                Console.WriteLine("Error: Assignment not found.");
            }
        }
    }

    public class ConsoleColors
    {
        public ConsoleColors() { }

        public static void MainUIColor()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
        }
        public static void InputColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void SuccessColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
        }
        public static void ErrorColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
        }
        public static void OtherColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
        }   
    }
}
