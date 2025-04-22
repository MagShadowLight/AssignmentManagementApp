namespace AssignmentManagementApp;

public class Assignment
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public bool IsComplete { get; private set; }

    public Assignment(string title, string description)
    {
        ValidTitleAndDescription(title, description);

        Title = title;
        Description = description;
    }

    public void Update(string newTitle, string newDescription)
    {
        ValidTitleAndDescription(newTitle, newDescription);

        Title = newTitle;
        Description = newDescription;
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
