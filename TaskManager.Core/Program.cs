namespace TaskManager.Core;

internal static class Program
{
    public static void Main(string[] args)
    {
        var issueDatabase = new IssueDatabase();
        var userDatabase = new UserDatabase();

        var user1 = User.CreateUser("Nikoloz Taturashvili", "tnicko@proton.me", "12345678", userDatabase);
        var user2 = User.CreateUser("Nikoloz Chachua", "nika2006@gmail.com", "12345678", userDatabase);
        
        var issue1 = Issue.CreateIssue("Make Task Manager", Priority.High, issueDatabase);
        
        issue1?.ChangeAssignee(user1);
        
        var issue2 = Issue.CreateIssue("Issue class for Task Manager", Priority.Medium, issueDatabase, "Create Issue class for Task Manager project.", user2);

        issue2?.ChangeStatus(Status.InProgress);
        
        issue1?.ChangeStatus(Status.InProgress);
        
        foreach (var issue in issueDatabase.GetAllIssues())
        {
            issue.Print();
        } 
    }
}