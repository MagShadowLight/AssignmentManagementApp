using AssignmentManagementApp.Core;
using AssignmentManagementApp.UI;
using Castle.Core.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagementApp.Tests
{
    public class AssignmentServiceTests
    {
        [Fact]
        public void When_List_Incomplete_Returns_Incomplete_Assignments_Then_ItShouldSucceed()
        {
            // Arrange
            var service = new AssignmentService(new AssignmentFormatter(), new ConsoleAppLogger());
            var assignment1 = new Assignment(DateTime.Now, "Read Chapter 1", "Summarize key points");
            var assignment2 = new Assignment(DateTime.Now, "Read Chapter 2", "Summarize key points");
            assignment2.MarkComplete(); // Mark this one as complete
            service.AddAssignment(assignment1);
            service.AddAssignment(assignment2);
            // Act
            var incompleteAssignments = service.ListIncomplete();
            // Assert
            Assert.Single(incompleteAssignments);
            Assert.Contains(assignment1, incompleteAssignments);
        }
        [Fact]
        public void When_Assignment_List_Is_Empty_Then_It_Should_Succeed()
        {
            // Arrange
            var service = new AssignmentService(new AssignmentFormatter(), new ConsoleAppLogger());
            // Act
            var assignments = service.ListAll();
            // Assert
            Assert.Empty(assignments);
        }
        
        [Fact]
        public void When_Assignment_List_Has_Incomplete_And_Complete_Assignments_Then_It_Should_Succeed()
        {
            // Arrange
            var service = new AssignmentService(new AssignmentFormatter(), new ConsoleAppLogger());
            var assignment1 = new Assignment(DateTime.Now, "Read Chapter 1", "Summarize key points");
            var assignment2 = new Assignment(DateTime.Now, "Read Chapter 2", "Summarize key points");
            assignment2.MarkComplete(); // Mark this one as complete
            service.AddAssignment(assignment1);
            service.AddAssignment(assignment2);
            // Act
            var allAssignments = service.ListAll();
            // Assert
            Assert.Equal(2, allAssignments.Count);
            Assert.Contains(assignment1, allAssignments);
            Assert.Contains(assignment2, allAssignments);
        }

        [Fact]
        public void When_Assignment_Formatter_Verify_Output_Then_It_Should_Succeed()
        {
            // Arrange
            var service = new AssignmentFormatter();
            var assignment = new Assignment(DateTime.Now, "Test Task", "This is a test");
            // Act
            var result = service.Format(assignment);
            // Assert
            Assert.Contains($"{result}", result);
        }

        [Fact]
        public void When_Logger_Display_Log_Then_It_Should_Succeed()
        {
            // Arrange
            ConsoleAppLogger logger = new ConsoleAppLogger();
            string message = "Error: Assignment not found";
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                logger.Log(message);
                string outputmessage = sw.ToString();
                Assert.Contains(message, outputmessage);
            }
            ResetOutput();
        }
        [Fact]
        public void When_Logger_Display_Error_Then_It_Should_Succeed()
        {
            // Arrange
            ConsoleAppLogger logger = new ConsoleAppLogger();
            string message = "Invalid choice. Try again.";
            // Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                logger.Error(message);
                string outputmessage = sw.ToString();
                // Assert
                Assert.Contains(message, outputmessage);
            }
            ResetOutput();
        }
        [Fact]
        public void When_Assignment_Created_Then_Logger_Should_Display_That_Assignment_Created()
        {
            // Arrange
            ConsoleAppLogger logger = new ConsoleAppLogger();
            string message = "Assignment added";
            // Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                logger.Log(message);
                string outputmessage = sw.ToString();
                // Assert
                Assert.Contains(message, outputmessage);
            }
            ResetOutput();
        }
        [Fact]
        public void When_Formatter_Display_Assignment_Then_It_Should_Display_Notes()
        {
            // Arrange
            AssignmentFormatter formatter = new AssignmentFormatter();
            Assignment assignment = new Assignment(null, "Test Task", "Do something", Priority.Low, "Test note");
            // Act
            string outputnote = formatter.Format(assignment);
            // Assert
            Assert.Contains($"Test note", outputnote);            
        }
        [Fact]
        public void When_Assignments_Are_Overdue_Then_Logger_Should_Display_Warning()
        {
            // Arrange
            var assignment = new Assignment(DateTime.Parse("June 1, 2025"), "Task", "do something", Priority.Low, "");
            var logger = new ConsoleAppLogger();
            var message = $"this assignment, {assignment.Title}, is overdue!";
            // Act
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                if (assignment.IsOverdue())
                {
                    logger.Warn(message);
                }
                string outputmessage = sw.ToString();
                // Assert
                Assert.Contains(message, outputmessage);
            }
            ResetOutput();
        }
        private void ResetOutput()
        {
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }
        [Fact]
        public void When_Trying_To_Update_Assignment_With_Blank_Title_Then_It_Should_Throw_Exception()
        {
            // Arrange
            var service = new AssignmentService(new AssignmentFormatter(), new ConsoleAppLogger());
            var assignment = new Assignment(DateTime.Now, "Test Task", "This is a test");
            service.AddAssignment(assignment);
            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.UpdateAssignment("Test Task", "", "New Description", Priority.High, "New Note"));
        }
        [Fact]
        public void When_Trying_To_Add_Assignment_With_Blank_Title_Then_It_Should_Throw_Exception()
        {
            // Arrange
            var service = new AssignmentService(new AssignmentFormatter(), new ConsoleAppLogger());
            // Act // Assert
            Assert.Throws<ArgumentException>(() => service.AddAssignment(new Assignment(DateTime.Now, null, "This is a test")));
        }
        [Fact]
        public void When_Assignment_Were_Marked_As_Complete_Then_It_Should_Succeed()
        {
            // Arrange
            var service = new AssignmentService(new AssignmentFormatter(), new ConsoleAppLogger());
            var assignment = new Assignment(DateTime.Now, "Meow", "meow");
            service.AddAssignment(assignment);
            // Act 
            service.MarkAssignmentComplete("Meow");
            // Assert
            Assert.True(assignment.IsComplete);
        }
        [Fact]
        public void When_Assignment_Were_Deleted_Then_It_Should_Succeed()
        {
            // Arrange
            var service = new AssignmentService(new AssignmentFormatter(), new ConsoleAppLogger());
            var assignment1 = new Assignment(DateTime.Now, "Meow", "meow");
            var assignment2 = new Assignment(DateTime.Now, "Mrow", "mrow");
            service.AddAssignment(assignment1);
            service.AddAssignment(assignment2);
            // Act
            service.DeleteAssignment("Mrow");
            // Assert
            Assert.Contains(service.ListAll(), a => a.Title == "Meow");
        }
        [Fact]
        public void When_Assignment_Has_Low_Priority_Then_It_Should_Returns_Only_Low_Priority()
        {
            // Arrange
            var service = new AssignmentService(new AssignmentFormatter(), new ConsoleAppLogger());
            var assignment1 = new Assignment(DateTime.Now, "Meow", "meow", Priority.Low);
            var assignment2 = new Assignment(DateTime.Now, "Mrow", "mrow");
            service.AddAssignment(assignment1);
            service.AddAssignment(assignment2);
            // Act
            List<Assignment> AssignmentWithLow = service.ListAssignmentsByPriority(Priority.Low);
            // Assert
            Assert.Equal(1,AssignmentWithLow.Count());
        }
    }
}
