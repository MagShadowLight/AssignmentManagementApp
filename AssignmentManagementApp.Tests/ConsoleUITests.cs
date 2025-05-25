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
            using var input = new StringReader("1\nSimple Task\nDo something simple\nM\n\n0");
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
            mockService.Setup(services => services.FindAssignmentByTitle("Simple Task")).Returns(new Assignment("Simple Task", "Do something simple"));

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
    }
}
