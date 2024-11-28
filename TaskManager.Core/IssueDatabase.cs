namespace TaskManager.Core;

public class IssueDatabase
{
    private readonly List<Issue> _issues = [];

    public void AddIssue(Issue issue)
    {
        ArgumentNullException.ThrowIfNull(issue);

        _issues.Add(issue);
    }

    public void RemoveIssue(string title)
    {
        var issue = GetIssueByTitle(title);
        if (issue != null)
        {
            _issues.Remove(issue);
        }
    }

    public Issue? GetIssueByTitle(string title)
    {
        return _issues.FirstOrDefault(i => 
            i.GetTitle().Equals(title, StringComparison.OrdinalIgnoreCase));
    }
    
    public Issue? GetIssueById(int id)
    {
        return _issues.FirstOrDefault(i => i.GetId() == id);
    }
    
    public List<Issue> GetAllIssues()
    {
        return _issues;
    }

    public bool UpdateIssueTitle(string currentTitle, string newTitle)
    {
        var issue = GetIssueByTitle(currentTitle);
        if (issue == null) return false;
        issue.ChangeTitle(newTitle);
        return true;
    }

    public bool UpdateIssueDescription(string title, string newDescription)
    {
        var issue = GetIssueByTitle(title);
        if (issue == null) return false;
        issue.ChangeDescription(newDescription);
        return true;
    }

    public bool UpdateIssuePriority(string title, Priority newPriority)
    {
        var issue = GetIssueByTitle(title);
        if (issue == null) return false;
        issue.ChangePriority(newPriority);
        return true;
    }

    public bool UpdateIssueAssignee(string title, User? newAssignee)
    {
        var issue = GetIssueByTitle(title);
        if (issue == null) return false;
        issue.ChangeAssignee(newAssignee);
        return true;
    }

    public bool UpdateIssueDue(string title, DateTime? newDue)
    {
        var issue = GetIssueByTitle(title);
        if (issue == null) return false;
        issue.ChangeDue(newDue);
        return true;
    }

    public bool UpdateIssueStatus(string title, Status newStatus)
    {
        var issue = GetIssueByTitle(title);
        if (issue == null) return false;
        issue.ChangeStatus(newStatus);
        return true;
    }
    
    public List<Issue> GetIssuesByPriority(Priority priority)
    {
        return _issues.Where(i => i.GetPriority() == priority).ToList();
    }
    
    public List<Issue> GetIssuesByDueDate(DateTime? due)
    {
        return _issues.Where(i => i.GetDue() == due).ToList();
    }
}