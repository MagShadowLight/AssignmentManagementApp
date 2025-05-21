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
        public void Constructor_ValidInput_ShouldCreateAssignment()
        {
            var assignment = new Assignment("Read Chapter 2", "Summarize key points");
            Assert.Equal("Read Chapter 2", assignment.Title);
            Assert.Equal("Summarize key points", assignment.Description);
        }

        [Fact]
        public void Constructor_BlankTitle_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => new Assignment("", "Valid description"));
        }

        [Fact]
        public void Update_BlankTitleOrDescription_ShouldThrowException()
        {
            var assignment = new Assignment("Read Chapter 2", "Summarize key points", Priority.Low);
            Assert.Throws<ArgumentException>(() => assignment.Update("Valid title", "", Priority.Low));
            Assert.Throws<ArgumentException>(() => assignment.Update("", "Valid description", Priority.Low));
        }
        [Fact]
        public void When_Assignment_Has_Default_Priority_Then_It_Should_Succeed()
        {
            var assignment = new Assignment("Task 1", "Details");
            Assert.Equal(Priority.Medium, assignment.Priority);
        }
        [Fact]
        public void When_Assignment_Has_High_Priority_Then_It_Should_Succeed()
        {
            var assignment = new Assignment("Urgent Task", "Do it now", Priority.High);
            Assert.Equal(Priority.High, assignment.Priority);
        }
    }
}
