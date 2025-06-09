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
        protected readonly IAssignmentService _assignmentService;
        protected readonly IAppLogger _logger;
        protected readonly IAssignmentFormatter _formatter;

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
                Console.WriteLine("4. List Assignments by Priority");
                Console.WriteLine("5. Mark Assignment as Complete");
                Console.WriteLine("6. Search Assignment by Title");
                Console.WriteLine("7. Update Assignment");
                Console.WriteLine("8. Delete Assignment");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");
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
                        ListAssignmentsByPriority();
                        break;
                    case "5":
                        ConsoleColors.MainUIColor();
                        MarkAssignmentComplete();
                        break;
                    case "6":
                        ConsoleColors.MainUIColor();
                        SearchAssignmentByTitle();
                        break;
                    case "7":
                        ConsoleColors.MainUIColor();
                        UpdateAssignment();
                        break;
                    case "8":
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
                        _logger.Error("Invalid choice. Try again");
                        break;
                }
            }
        }

        

        public void AddAssignment()
        {
            var title = UserInput.GetStringInput("Enter assignment title: ");
            var description = UserInput.GetStringInput("Enter assignment description: ");
            var dueDate = UserInput.GetDateInput("Enter assignment due date (MMM DD, YYYY): ");
            var priority = UserInput.GetPriority("Enter assignment priority. Use (L)ow, (M)edium, or (H)igh: ");
            var notes = UserInput.GetStringInput("Enter assignment notes: ");
            try
            {
                var assignment = new Assignment(dueDate, title, description, priority, notes);
                _assignmentService.AddAssignment(assignment);
                ConsoleColors.SuccessColor();
                _logger.Log("Assignment added");
            }
            catch (Exception ex)
            {
                ConsoleColors.ErrorColor();
                _logger.Error(ex.Message);
            }
        }

        private void ListAllAssignments()
        {
            ConsoleColors.AssignmentColor();
            var assignments = _assignmentService.ListAll();
            if (assignments.Count == 0)
            {
                Console.WriteLine("No assignments found.");
                return;
            }

            foreach (var assignment in assignments)
            {
                ConsoleColors.AssignmentColor();
                Console.WriteLine(_formatter.Format(assignment));
                if (assignment.IsOverdue())
                {
                    ConsoleColors.WarningColor();
                    _logger.Warn($"this assignment, {assignment.Title}, is overdue!");
                }
            }
        }

        private void ListIncompleteAssignments()
        {
            ConsoleColors.AssignmentColor();
            var assignments = _assignmentService.ListIncomplete();
            if (assignments.Count == 0)
            {
                Console.WriteLine("No incomplete assignments found.");
                return;
            }

            foreach(var assignment in assignments)
            {
                Console.WriteLine(_formatter.Format(assignment));
                if (assignment.IsOverdue())
                {
                    ConsoleColors.WarningColor();
                    _logger.Warn($"this assignment, {assignment.Title}, is overdue!");
                }
            }
        }

        private void ListAssignmentsByPriority()
        {
            Priority priority = UserInput.GetPriority("Enter the priority. Use (L)ow, (M)edium, or (H)igh: ");
            ConsoleColors.AssignmentColor();
            var assignments = _assignmentService.ListAssignmentsByPriority(priority);
            if (assignments.Count == 0)
            {
                Console.WriteLine("No assignments found.");
                return;
            }

            foreach (var assignment in assignments)
            {
                Console.WriteLine(_formatter.Format(assignment));
                if (assignment.IsOverdue())
                {
                    ConsoleColors.WarningColor();
                    _logger.Warn($"this assignment, {assignment.Title}, is overdue!");
                }
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
                _logger.Log("Assignment marked as complete.");
            } else
            {
                ConsoleColors.ErrorColor();
                _logger.Error("Assignment not found.");
                
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
                Console.WriteLine("Found: " + _formatter.Format(assignment));
            } else
            {
                ConsoleColors.ErrorColor();
                _logger.Error("Assignment not found.");
            }
        }

        private void UpdateAssignment()
        {
            var oldTitle = UserInput.GetStringInput("Enter the current title of the assignment to update: ");
            var newTitle = UserInput.GetStringInput("Enter the new title: ");
            var newDescription = UserInput.GetStringInput("Enter the new description: ");
            Priority newPriority;
            var newNotes = string.Empty;
            try
            {
                newPriority = UserInput.GetPriority("Enter the new priority. Use (L)ow, (M)edium, or (H)igh: ");
                newNotes = UserInput.GetStringInput("Enter the new notes: ");
                _assignmentService.UpdateAssignment(oldTitle, newTitle, newDescription, newPriority, newNotes);
                ConsoleColors.SuccessColor();
                _logger.Log("Assignment updated successfully.");
            } catch (Exception ex)
            {
                ConsoleColors.ErrorColor();
                _logger.Error(ex.Message);
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
                _logger.Log("Assignment deleted successfully.");
            }
            else
            {
                ConsoleColors.ErrorColor();
                _logger.Error("Assignment not found.");
            }
        }

        public Priority ConvertToPriority(string priorityvalue)
        {
           
            return priorityvalue switch
            {
                "L" => Priority.Low,
                "M" => Priority.Medium,
                "H" => Priority.High,
                _ => throw new Exception("Invalid Priority Input. Use (L)ow, (M)edium, or (H)igh")
            };
            
            
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
            Console.ForegroundColor = ConsoleColor.DarkGray;
        }
        public static void AssignmentColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void WarningColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
    }

    public class UserInput
    {
        public UserInput() { }

        public static string GetStringInput(string prompt)
        {
            Console.Write(prompt);
            ConsoleColors.InputColor();
            var input = Console.ReadLine();
            ConsoleColors.MainUIColor();
            return input;
        }

        public static DateTime? GetDateInput(string message)
        {
            Console.Write(message);
            ConsoleColors.InputColor();
            var dateInput = Console.ReadLine();
            ConsoleColors.MainUIColor();
            try
            {
                return DateTime.Parse(dateInput);
            }
            catch (FormatException)
            {
                return null;
            }
        }

        public static Priority GetPriority(string message)
        {
            Console.Write(message);
            ConsoleColors.InputColor();
            var priorityInput = Console.ReadLine()?.ToUpper();
            ConsoleColors.MainUIColor();
            return priorityInput switch
            {
                "L" => Priority.Low,
                "M" => Priority.Medium,
                "H" => Priority.High,
                _ => throw new Exception("Invalid Priority Input. Use (L)ow, (M)edium, or (H)igh")
            };
        }
    }

    
}
