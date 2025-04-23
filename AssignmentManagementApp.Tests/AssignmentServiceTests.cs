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
            var service = new AssignmentService();
            var assignment1 = new Assignment("Read Chapter 1", "Summarize key points");
            var assignment2 = new Assignment("Read Chapter 2", "Summarize key points");
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
            var service = new AssignmentService();

            // Act
            var assignments = service.ListAll();

            // Assert
            Assert.Empty(assignments);
        }

        [Fact]
        public void When_Assignment_List_Has_Incomplete_And_Complete_Assignments_Then_It_Should_Succeed()
        {
            // Arrange
            var service = new AssignmentService();
            var assignment1 = new Assignment("Read Chapter 1", "Summarize key points");
            var assignment2 = new Assignment("Read Chapter 2", "Summarize key points");
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
    }
}
