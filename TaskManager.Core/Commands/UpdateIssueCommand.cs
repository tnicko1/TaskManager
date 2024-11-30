using TaskManager.Core.Enums;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Models;

namespace TaskManager.Core.Commands;

public class UpdateIssueCommand(
    string title,
    string? description,
    Guid id,
    Priority priority,
    DateTime? due,
    DateTime? finishedAt,
    User? assignee,
    Status status,
    DateTime? createdAt)
{
    private string Title { get; set; } = title;
    private string? Description { get; set; } = description;
    private Guid Id { get; set; } = id;
    private Priority Priority { get; set; } = priority;
    private DateTime? Due { get; set; } = due;
    private DateTime? FinishedAt { get; set; } = finishedAt;
    private User? Assignee { get; set; } = assignee;
    private Status Status { get; set; } = status;
    private DateTime? CreatedAt { get; set; } = createdAt;
    
    public void Execute()
    {
        Validate();
        var issue = new Issue
        {
            Title = Title,
            Description = Description,
            Id = Id,
            Priority = Priority,
            Due = Due,
            Assignee = Assignee,
            Status = Status,
            FinishedAt = Status == Status.Done ? DateTime.Now : FinishedAt,
            CreatedAt = CreatedAt
        };
        
        Issue.Database?.UpdateIssue(issue);
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

        if (FinishedAt is not null && FinishedAt < DateTime.Now)
        {
            throw new ArgumentException("Finished at date must be greater than current date.");
        }
        
        if (Issue.Database?.GetIssueById(Id) is null)
        {
            throw new IssueNotFoundException("Issue does not exist.");
        }
        
        var issue = Issue.Database?.GetIssueById(Id);

        switch (issue?.Status)
        {
            case Status.Done:
                throw new InvalidStatusChangeException("Cannot change status when issue is already Done.");
            case Status.InProgress when Status == Status.Todo:
                throw new InvalidStatusChangeException("Cannot change status from In Progress to Todo.");
            case Status.Todo when Status == Status.Done:
                throw new InvalidStatusChangeException("Cannot change status from Todo to Done.");
        }
    }
}