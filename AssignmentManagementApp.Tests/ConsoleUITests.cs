using AssignmentManagementApp.Core;
using AssignmentManagementApp.Core.Interfaces;
using AssignmentManagementApp.UI;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagementApp.Tests
{
    public class ConsoleUITests
    {
        private Mock<IAppLogger> loggerService = new();
        private Mock<IAssignmentFormatter> formatterService = new();

        [Fact]
        public void When_Assignment_Added_Then_It_Should_Succeed()
        {
            // Arrange
            var mockService = new Mock<IAssignmentService>();
            var consoleUI = new ConsoleUI(mockService.Object, loggerService.Object, formatterService.Object);
            using var input = new StringReader("1\nSimple Task\nDo something simple\n\nM\n\n0");
            Console.SetIn(input);

            // Act

            consoleUI.Run();

            // Assert
            mockService.Verify(user => user.AddAssignment(It.Is<Assignment>(assignment => assignment.Title == "Simple Task" && assignment.Description == "Do something simple")), Times.Once());
        }
        [Fact]
        public void When_User_Search_For_Title_Then_It_Should_Return_With_Title()
        {
            // Arrange
            var mockService = new Mock<IAssignmentService>();
            mockService.Setup(services => services.FindAssignmentByTitle("Simple Task")).Returns(new Assignment(DateTime.Now, "Simple Task", "Do something simple"));

            var consoleUI = new ConsoleUI(mockService.Object, loggerService.Object, formatterService.Object);

            using var userInput = new StringReader("6\nSimple Task\n0");
            Console.SetIn(userInput);

            // Act
            consoleUI.Run();

            // Assert
            mockService.Verify(user => user.FindAssignmentByTitle("Simple Task"), Times.Once());
        }

        [Fact]
        public void When_Assignment_Were_Deleted_Then_It_Should_Succeed()
        {
            // Arrange
            var mockService = new Mock<IAssignmentService>();
            mockService.Setup(service => service.DeleteAssignment("Simple Task")).Returns(true);
            var consoleUI = new ConsoleUI(mockService.Object, loggerService.Object, formatterService.Object);
            using var userInput = new StringReader("8\nSimple Task\n0");
            Console.SetIn(userInput);
            // Act
            consoleUI.Run();
            // Assert
            mockService.Verify(user => user.DeleteAssignment("Simple Task"), Times.Once());
        }
        [Fact]
        public void When_User_Enter_Priority_Then_It_Should_Return_Assignments()
        {
            // Arrange
            var mockService = new Mock<IAssignmentService>();
            var assignment = new Assignment(DateTime.Now, "Task", "Do something", Priority.Low, "");
            var consoleUI = new ConsoleUI(mockService.Object, loggerService.Object, formatterService.Object);
            List<Assignment> assignments = new List<Assignment>();
            assignments.Add(assignment);
            mockService.Setup(service => service.ListAssignmentsByPriority(Priority.Low)).Returns(assignments);
            using var userInput = new StringReader("4\nL\n0");
            Console.SetIn(userInput);
            // Act
            consoleUI.Run();
            // Assert
            mockService.Verify(user => user.ListAssignmentsByPriority(Priority.Low), Times.Once());
        }
        [Fact]
        public void When_User_Update_Assignment_Then_It_Should_Succeed()
        {
            // Arrange
            var mockService = new Mock<IAssignmentService>();
            var assignment = new Assignment(DateTime.Now, "Task", "Do something", Priority.High, "");
            mockService.Setup(service => service.UpdateAssignment(assignment.Title, "Simple Task", "Do something simple", Priority.Low, ""));
            var consoleUI = new ConsoleUI(mockService.Object, loggerService.Object, formatterService.Object);
            // Act
            using var userInput = new StringReader("7\nTask\nSimple Task\nDo something simple\nL\n\n0");
            Console.SetIn(userInput);
            consoleUI.Run();
            // Assert
            mockService.Verify(user => user.UpdateAssignment("Task", "Simple Task", "Do something simple", Priority.Low, ""), Times.Once);
        }
        [Fact]
        public void When_User_Mark_Assignment_As_Completed_Then_It_Should_Succeed()
        {
            // Arrange
            var mockService = new Mock<IAssignmentService>();
            var assignment = new Assignment(DateTime.Now, "Task", "Do something", Priority.Low, "");
            mockService.Setup(service => service.MarkAssignmentComplete(assignment.Title)).Returns(true);
            var consoleUI = new ConsoleUI(mockService.Object, loggerService.Object, formatterService.Object);
            // Act
            using var userInput = new StringReader("5\nTask\n0");
            Console.SetIn(userInput);
            consoleUI.Run();
            // Assert
            mockService.Verify(user => user.MarkAssignmentComplete("Task"), Times.Once);
        }
        [Fact]
        public void When_User_Select_List_All_Assignments_Then_It_Should_Return_Assignments_List()
        {
            // Arrange
            var mockService = new Mock<IAssignmentService>();
            var consoleUI = new ConsoleUI(mockService.Object, loggerService.Object, formatterService.Object);
            List<Assignment> assignments = new List<Assignment>
            {
                new Assignment(DateTime.Now, "Test Task", "meow"),
                new Assignment(DateTime.Now, "Test Task 2", "mrow")
            };
            mockService.Setup(service => service.ListAll()).Returns(assignments);
            using var userInput = new StringReader("2\n0");
            Console.SetIn(userInput);
            // Act
            consoleUI.Run();
            // Assert
            mockService.Verify(user => user.ListAll(), Times.Once());
        }
        [Fact]
        public void When_User_Select_List_Incomplete_Assignments_Then_It_Should_Return_List_Of_Incomplete_Assignments()
        {
            // Arrange
            var mockService = new Mock<IAssignmentService>();
            var consoleUI = new ConsoleUI(mockService.Object, loggerService.Object, formatterService.Object);
            List<Assignment> incompleteAssignments = new List<Assignment>();
            var assignment1 = new Assignment(DateTime.Now, "Test Task", "meow", Priority.Medium, "");
            var assignment2 = new Assignment(DateTime.Now, "Test Task 2", "mrow", Priority.High, "");
            var assignment3 = new Assignment(DateTime.Now, "Test Task 3", "mrrp", Priority.Low, "");
            assignment2.MarkComplete();
            incompleteAssignments.Add(assignment1);
            incompleteAssignments.Add(assignment2);
            incompleteAssignments.Add(assignment3);
            // Act
            mockService.Setup(service => service.ListIncomplete()).Returns(incompleteAssignments);
            using var userInput = new StringReader("3\n0");
            Console.SetIn(userInput);
            consoleUI.Run();
            // Assert
            mockService.Verify(user => user.ListIncomplete(), Times.Once());
        }
    }
}
