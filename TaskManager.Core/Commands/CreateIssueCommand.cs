using TaskManager.Core.Enums;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Models;

namespace TaskManager.Core.Commands;

public class CreateIssueCommand(
    string title,
    string description,
    Guid id,
    Priority priority,
    DateTime? due,
    DateTime? finishedAt,
    User? assignee)
{
    private string Title { get; set; } = title;
    private string? Description { get; set; } = description;
    private Guid Id { get; set; } = id;
    private Priority Priority { get; set; } = priority;
    private DateTime? Due { get; set; } = due;
    private DateTime? FinishedAt { get; set; } = finishedAt;
    private User? Assignee { get; set; } = assignee;
    
    public void Execute()
    {
        Validate();
        var issue = new Issue
        {
            Title = Title,
            Description = Description,
            Id = Id,
            Priority = Priority,
            Status = Status.Todo,
            CreatedAt = DateTime.Now,
            Due = Due,
            FinishedAt = FinishedAt,
            Assignee = Assignee
        };
        if (issue is null)
        {
            throw new IssueNullException();
        }
        
        Issue.Database?.AddIssue(issue);
    }
    
    private void Validate()
    {
        if (Title.Length is < 1 or > 100)
        {
            throw new ArgumentException("Title must be between 1 and 100 characters.");
        }
        
        if (Description is { Length: < 1 or > 4000 })
        {
            throw new ArgumentException("Description must be between 1 and 4000 characters.");
        }
        
        if (Due <= DateTime.Now)
        {
            throw new ArgumentException("Due date must be in the future.");
        }
        
        if (Issue.Database?.GetIssueById(Id) != null)
        {
            throw new ArgumentException("Issue with this id already exists.");
        }
    }
}