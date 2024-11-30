using TaskManager.Core.Enums;
using TaskManager.Core.Services.Implementations;

namespace TaskManager.Core.Models;

public class Issue {
    public required string Title { get; set; }
    public string? Description { get; set; }
    public User? Assignee { get; set; }
    public required Priority Priority { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? Due { get; set; }
    public DateTime? FinishedAt { get; set; }
    public Status Status { get; set; }
    public Guid Id{ get; set; }
    
    public static IssueDatabase? Database { get; set; }
}
