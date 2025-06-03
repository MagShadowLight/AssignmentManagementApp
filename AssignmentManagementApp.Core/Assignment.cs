namespace AssignmentManagementApp.Core;

public class Assignment
{
    public int Id { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public DateTime? DueDate { get; set; }
    public bool IsComplete { get; set; }
    public string? Notes { get; set; }

    public Assignment(DateTime? dueDate, string title, string description, Priority priority = Priority.Medium, string notes = "")
    {
        ValidTitleAndDescription(title, description);

        Title = title;
        Description = description;
        Priority = priority;
        Notes = notes;
        DueDate = dueDate;
        IsComplete = false;

    }

    public void Update(string newTitle, string newDescription, Priority newPriority, string newNotes)
    {
        ValidTitleAndDescription(newTitle, newDescription);

        Title = newTitle;
        Description = newDescription;
        Priority = newPriority;
        Notes = newNotes;
    }

    private void ValidTitleAndDescription(string title, string description)
    {
        Validate(title, nameof(title));
        Validate(description, nameof(description));
    }

    private void Validate(string input, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException($"{fieldName} cannot be blank or whitespace.");
    }

    public void MarkComplete()
    {
        IsComplete = true;
    }

    public bool IsOverdue()
    {
        if (!IsComplete && DueDate != null)
        {
            return DueDate.Value < DateTime.Now;
        } else
        {
            return false;
        }
    }
}
