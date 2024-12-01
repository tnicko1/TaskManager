using TaskManager.Core.Models;

namespace TaskManager.Core.Commands;

public class DeleteIssueCommand(Guid id)
{
    private Guid Id { get; set; } = id;
    public void Execute()
    {
        Validate();
        {
            Issue.Database?.DeleteIssue(Id);
        }
    }
    
    private void Validate()
    {
        if (Issue.Database == null)
        {
            throw new InvalidOperationException("Issue database is not initialized.");
        }

        var issue = Issue.Database.GetIssueById(Id);
        if (issue == null)
        {
            throw new ArgumentException("Issue not found.");
        }
    }
}