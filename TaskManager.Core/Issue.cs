namespace TaskManager.Core;

public class Issue
{
    private string Title { get; set; }
    private string Description { get; set; }
    private User? Assignee { get; set; }
    private Priority Priority { get; set; }
    private DateTime CreatedAt { get; set; }
    private DateTime? Due { get; set; }
    private DateTime? FinishedAt { get; set; }
    private Status Status { get; set; }

    private Issue(string title, Priority priority, string description = "", User? assignee = null, DateTime? due = null)
    {
        Title = title;
        Priority = priority;
        Description = description;
        Assignee = assignee;
        Due = due;
        Status = Status.Todo;
        CreatedAt = DateTime.Now;
        FinishedAt = null;
    }
    
    public static Issue? CreateIssue(string title, Priority priority, IssueDatabase database, string description = "", User? assignee = null, DateTime? due = null)
    {
        try
        {
            if (ValidateTitle(title) && ValidateDescription(description) && ValidateDueDate(due))
            {
                var issue = new Issue(title, priority, description, assignee, due);
                database.AddIssue(issue);
                return issue;
            }
        }
        catch (ArgumentException e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(e.Message);
            Console.ResetColor();
        }

        return null;
    }
    
    private static bool ValidateTitle(string title)
    {
        if (title.Length is > 1 and <= 100)
        {
            return true;
        }
        throw new ArgumentException("Title must be between 1 and 100 characters.");
    }
    
    private static bool ValidateDescription(string description)
    {
        if (description == "") return true;
        if (description.Length is <= 1 or > 4000) throw new ArgumentException("Description must be between 1 and 4000 characters.");
        return true;
    }
    
    private static bool ValidateDueDate(DateTime? due)
    {
        if (due == null) return true;
        if (due < DateTime.Now) throw new ArgumentException("Due date must be in the future.");
        return true;
    }
    
    
    public void ChangeTitle(string newTitle)
    {
        if (ValidateTitle(newTitle))
        {
            Title = newTitle;
        }
    }
    
    public void ChangeDescription(string newDescription)
    {
        if (ValidateDescription(newDescription))
        {
            Description = newDescription;
        }
    }
    
    public void ChangePriority(Priority newPriority)
    {
        Priority = newPriority;
    }
    
    public void ChangeAssignee(User newAssignee)
    {
        Assignee = newAssignee;
    }
    
    public void ChangeDue(DateTime? newDue)
    {
        if (ValidateDueDate(newDue))
        {
            Due = newDue;
        }
    }
    
    public void ChangeStatus(Status newStatus)
    {
        switch (Status)
        {
            case Status.Todo when newStatus == Status.InProgress:
                break;
            case Status.InProgress when newStatus == Status.Done:
                FinishedAt = DateTime.Now;
                break;
            case Status.InProgress when newStatus == Status.Todo:
                throw new ArgumentException("Cannot change status from In Progress to Todo.");
            case Status.Done:
                throw new ArgumentException("Cannot change status of a finished issue.");
            default:
                throw new ArgumentException("Invalid status change.");
        }

        Status = newStatus;
    }

    public void Print()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Description: {Description}");
        Console.WriteLine($"Priority: {Priority}");
        Console.WriteLine($"Assignee: {Assignee?.GetName() ?? "Unassigned"}");
        Console.WriteLine($"Created At: {CreatedAt}");
        Console.WriteLine(Due != null ? $"Due: {Due}" : "Due: No due date.");
        Console.WriteLine(FinishedAt != null ? $"Finished At: {FinishedAt}" : "Finished At: Not finished yet.");
        Console.WriteLine($"Status: {Status}");
    }
    
    public string GetTitle()
    {
        return Title;
    }
    
    public string GetDescription()
    {
        return Description;
    }
    
    public User? GetAssignee()
    {
        return Assignee;
    }
    
    public Priority GetPriority()
    {
        return Priority;
    }
    
    public DateTime GetCreatedAt()
    {
        return CreatedAt;
    }
    
    public DateTime? GetDue()
    {
        return Due;
    }
    
    public DateTime? GetFinishedAt()
    {
        return FinishedAt;
    }
    
    public Status GetStatus()
    {
        return Status;
    }
}
