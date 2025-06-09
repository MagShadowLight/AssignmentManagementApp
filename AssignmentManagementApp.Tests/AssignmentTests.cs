using AssignmentManagementApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentManagementApp.Tests
{
    public class AssignmentTests
    {
        [Fact]
        public void When_Constructor_Valid_Input_Then_It_Should_Create_Assignment()
        {
            var assignment = new Assignment(DateTime.Now, "Read Chapter 2", "Summarize key points");
            Assert.Equal("Read Chapter 2", assignment.Title);
            Assert.Equal("Summarize key points", assignment.Description);
        }

        [Fact]
        public void When_Constructor_Has_Blank_Title_Then_It_Should_Throw_Exception()
        {
            Assert.Throws<ArgumentException>(() => new Assignment(DateTime.Now, "", "Valid description"));
        }

        [Fact]
        public void When_Update_Has_Blank_Title_Or_Description_Then_It_Should_Throw_Exception()
        {
            var assignment = new Assignment(DateTime.Now, "Read Chapter 2", "Summarize key points", Priority.Low);
            Assert.Throws<ArgumentException>(() => assignment.Update("Valid title", "", Priority.Low, ""));
            Assert.Throws<ArgumentException>(() => assignment.Update("", "Valid description", Priority.Low, ""));
        }
        [Fact]
        public void When_Assignment_Has_Default_Priority_Then_It_Should_Succeed()
        {
            var assignment = new Assignment(DateTime.Now, "Task 1", "Details");
            Assert.Equal(Priority.Medium, assignment.Priority);
        }
        [Fact]
        public void When_Assignment_Has_High_Priority_Then_It_Should_Succeed()
        {
            var assignment = new Assignment(DateTime.Now, "Urgent Task", "Do it now", Priority.High);
            Assert.Equal(Priority.High, assignment.Priority);
        }
        [Fact]
        public void When_Assignment_Created_Then_Notes_Should_Stored()
        {
            // Arrange
            var assignment = new Assignment(DateTime.Now, "Task", "Do something", Priority.Low, "Test Note");
            // Act Assert
            Assert.Equal("Test Note", assignment.Notes);
        }
        [Fact]
        public void When_Assignment_Are_OverDue_Then_It_Should_Succeed()
        {
            // Arrange
            var assignment = new Assignment(DateTime.Parse("5/20/2025"), "Task", "Do something", Priority.Low, "Note");
            // Act
            bool Overdue = assignment.IsOverdue();
            // Assert
            Assert.True(Overdue);
        }
        [Fact]
        public void When_Assignment_Are_Completed_Then_Overdue_Should_Be_False()
        {
            // Arrange
            var assignment = new Assignment(DateTime.Parse("5/20/2025"), "Task", "Do something", Priority.Low, "Note");
            assignment.MarkComplete();
            // Act
            bool Overdue = assignment.IsOverdue();
            // Assert
            Assert.False(Overdue);
        }
        [Fact]
        public void When_Assignment_Created_Then_It_Should_Contains_DueDate()
        {
            // Arrange
            var assignment = new Assignment(DateTime.Today, "Task", "Do something", Priority.Low, "");
            // Act  // Assert
            Assert.Equal(DateTime.Today, assignment.DueDate);
        }
        [Fact]
        public void When_Assignment_Create_Without_DueDate_Then_It_Should_Succeed()
        {
            // Arrange
            var assignment = new Assignment(null, "Task", "Do something", Priority.Low, "");
            // Act // Assert
            Assert.Null(assignment.DueDate);
        }
        [Fact]
        public void When_Assignment_Created_Then_IsComplete_Property_Should_Set_To_False()
        {
            // Arrange
            var assignment = new Assignment(DateTime.Now, "Task", "Do something", Priority.Low, "");
            // Act // Assert
            Assert.False(assignment.IsComplete);
        }
    }
}
