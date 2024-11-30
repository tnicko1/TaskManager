using TaskManager.Core.Models;

namespace TaskManager.Core.Services.Abstractions;

public interface IIssueDatabase
{
    void AddIssue(Issue? issue);
    Issue? GetIssueById(Guid issueId);
    IEnumerable<Issue?> GetAllIssues();
    void UpdateIssue(Issue issue);
    void DeleteIssue(Guid issueId);
}