using TaskManager.Core.Enums;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Models;
using TaskManager.Core.Services.Abstractions;

namespace TaskManager.Core.Services.Implementations;

public class IssueDatabase : IIssueDatabase
{
    private readonly List<Issue?> _issues = [];

    public void AddIssue(Issue? issue)
    {
        _issues.Add(issue);
    }

    public Issue? GetIssueById(Guid issueId)
    {
        return _issues.FirstOrDefault(i => i?.Id == issueId);
    }
    
    public IEnumerable<Issue?> GetIssuesByTitle(string title)
    {
        return _issues.Where(i =>
            i?.Title.IndexOf(title, StringComparison.OrdinalIgnoreCase) >= 0);
    }
    
    public IEnumerable<Issue?> GetIssuesByPriority(Priority priority)
    {
        return _issues.Where(i => i?.Priority == priority);
    }
    
    public IEnumerable<Issue?> GetIssuesByStatus(Status status)
    {
        return _issues.Where(i => i?.Status == status);
    }
    
    public IEnumerable<Issue?> GetIssuesByAssignee(User assignee)
    {
        return _issues.Where(i => i?.Assignee == assignee);
    }
    
    public IEnumerable<Issue?> GetIssuesByDueDate(DateTime dueDate)
    {
        return _issues.Where(i => i?.Due <= dueDate);
    }
    
    public IEnumerable<Issue?> GetIssuesByFinishedDate(DateTime finishedDate)
    {
        return _issues.Where(i => i?.FinishedAt == finishedDate);
    }

    public IEnumerable<Issue?> GetAllIssues()
    {
        return _issues;
    }

    public void UpdateIssue(Issue issue)
    {
        var existingIssueIndex = _issues.FindIndex(i => i?.Id == issue.Id);
        if (existingIssueIndex == -1)
        {
            throw new IssueNotFoundException();
        }

        _issues[existingIssueIndex] = issue;
    }

    public void DeleteIssue(Guid issueId)
    {
        var issue = _issues.FirstOrDefault(i => i?.Id == issueId);
        if (issue == null)
        {
            throw new IssueNotFoundException();
        }

        _issues.Remove(issue);
    }
}