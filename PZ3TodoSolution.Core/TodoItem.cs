namespace PZ3TodoSolution.Core;

public class TodoItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; } = string.Empty;
    public bool IsDone { get; private set; }

    // Needed for JSON deserialization
    public TodoItem() { }

    public TodoItem(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title required", nameof(title));

        Title = title.Trim();
    }

    public void MarkDone() => IsDone = true;
    public void MarkUndone() => IsDone = false;

    public void Rename(string newTitle)
    {
        if (string.IsNullOrWhiteSpace(newTitle))
            throw new ArgumentException("Title required", nameof(newTitle));

        Title = newTitle.Trim();
    }
}
