# Change Management Plan:
Student Name: William Rose
Date Submitted: 6/3/2025

## Purpose of Change:
Fixing the notes not saved in assignment, not displayed to user, IsOverdue incorrectly flagged completed task, and logging missing. It matter because the behavior was not expecting what the result should be.

## Summary of Changes:
- Assignment.cs
	- Assignment constructor to include notes
	- modified IsOverdue() to include complete as a check
- AssgnmentFormatter.cs
	- Include note field to Format()
- AssignmentService.cs	
- ILogger.cs
	- Include Warn and Error methods
- ConsoleAppLogger.cs
	- Include Warn and Error methods
- AssignmentServiceTest.cs
	- Added unit test to test if logger are injected
	- Added unit test to see if formatter display note field
- AssignmentTest.cs
	- Added unit test to see if the assignment stored note field properly
	- Added unit test to see if completed assignment did not mark as overdue

## TDD Process
- What test failed first?
storing the note field.
- What did I change?
Include the note field in Assignment constructor.
- What confirmed the fix?
Note field was not stored properly, resulting in a null value or empty value.

## Additional Coverage
- Display the warning when assignments are overdue

## Challenges
- Where did you get stuck?
injecting a logger properly.
- How did you resolve it?
used the stringwriter to get the logger into a string.

## Recommendations
- Any Tech Debt uncovered
some of the methods are too long like UpdateAssignment() in ConsoleUI.cs
- Suggestions for future devs?
Separate the long methods into smaller methods.