using Xunit;
using AssignmentManagementApp;

namespace AssignmentManagementApp.Tests;

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
        var assignment = new Assignment("Read Chapter 2", "Summarize key points");
        Assert.Throws<ArgumentException>(() => assignment.Update("Valid title", ""));
        Assert.Throws<ArgumentException>(() => assignment.Update("", "Valid description"));
    }
    [Fact]
    public void When_List_IncompleteAssignments_Should_ReturnOnlyIncompleteAssignments()
    {
        var assignmentService = new AssignmentService();
        var assignment1 = new Assignment("Read Chapter 2", "Summarize key points");
        var assignment2 = new Assignment("Read Chapter 3", "Summarize key points");
        assignment1.MarkComplete();

        assignmentService.AddAssignment(assignment1);
        assignmentService.AddAssignment(assignment2);

        var incompleteAssignments = assignmentService.ListIncomplete();

        Assert.Single(incompleteAssignments);
        Assert.Equal(assignment2, incompleteAssignments[0]);
    }
    [Fact]
    public void When_Assignment_List_Empty_Should_ReturnEmptyList()
    {
        var assignmentService = new AssignmentService();
        var assignments = assignmentService.ListAll();
        

        Assert.Empty(assignments);
    }

    [Fact]
    public void When_List_CompleteAndIncompleteAssignments_Should_ReturnAllAssignments()
    {
        var assignmentService = new AssignmentService();
        var assignment1 = new Assignment("Read Chapter 2", "Summarize key points");
        var assignment2 = new Assignment("Read Chapter 3", "Summarize key points");
        assignment1.MarkComplete();

        assignmentService.AddAssignment(assignment1);
        assignmentService.AddAssignment(assignment2);

        var allAssignments = assignmentService.ListAll();

        Assert.Equal(2, allAssignments.Count);
        Assert.Contains(allAssignments, a => a.Title == "Read Chapter 2");
        Assert.Contains(allAssignments, a => a.Title == "Read Chapter 3");
    }
}
