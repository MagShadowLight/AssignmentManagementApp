namespace AssignmentManagementApp.Core;

public class Assignment
{
    public int Id { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public bool IsComplete { get; set; }

    public Assignment(string title, string description, Priority priority = Priority.Medium)
    {
        ValidTitleAndDescription(title, description);

        Title = title;
        Description = description;
        Priority = priority;
    }

    public void Update(string newTitle, string newDescription, Priority newPriority)
    {
        ValidTitleAndDescription(newTitle, newDescription);

        Title = newTitle;
        Description = newDescription;
        Priority = newPriority;
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
}
