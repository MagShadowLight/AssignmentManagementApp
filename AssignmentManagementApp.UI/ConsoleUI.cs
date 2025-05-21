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
                Console.WriteLine("4. List Assignments by Priority");
                Console.WriteLine("5. Mark Assignment as Complete");
                Console.WriteLine("6. Search Assignment by Title");
                Console.WriteLine("7. Update Assignment");
                Console.WriteLine("8. Delete Assignment");
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
            Console.Write("Enter assignment priority. Use L, M, or H: ");
            ConsoleColors.InputColor();
            try
            {
                var priorityInput = Console.ReadLine()?.ToUpper();
                var priorityOutput = ConvertToPriority(priorityInput);



                var a = new Assignment(title, description, (Priority)priorityOutput);
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
            catch (Exception ex)
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
                Console.WriteLine(_formatter.Format(assignment));
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
                Console.WriteLine(_formatter.Format(assignment));
            }
        }

        private void ListAssignmentsByPriority()
        {
            Priority priorityOutput = (Priority)(-1);
            Console.Write("Enter the priority. Use (L)ow, (M)edium, or (H)igh: ");
            ConsoleColors.InputColor();
            try
            {
                var priorityInput = Console.ReadLine()?.ToUpper();
                priorityOutput = ConvertToPriority(priorityInput);
            } catch (Exception ex)
            {
                ConsoleColors.ErrorColor();
                Console.WriteLine(ex.Message);
                return;
            }
            var assignments = _assignmentService.ListAssignmentsByPriority(priorityOutput);
            if (assignments.Count == 0)
            {
                Console.WriteLine("No assignments found.");
                return;
            }

            foreach (var assignment in assignments)
            {
                Console.WriteLine(_formatter.Format(assignment));
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
                Console.WriteLine("Found: " + _formatter.Format(assignment));
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
            ConsoleColors.MainUIColor();
            Console.Write("Enter the new priority from Low (0) to High (2): ");
            ConsoleColors.InputColor();
            try
            {
                var priorityInput = Console.ReadLine()?.ToUpper();
                var priorityOutput = ConvertToPriority(priorityInput);
                if (priorityOutput == (Priority)3)
                    throw new("Error: Invalid Priority Input.");
                if (_assignmentService.UpdateAssignment(oldTitle, newTitle, newDescription, priorityOutput))
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
            Console.ForegroundColor = ConsoleColor.Yellow;
        }   
    }
}
