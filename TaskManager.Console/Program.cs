using TaskManager.Core;
using TaskManager.Core.Commands;
using TaskManager.Core.Enums;
using TaskManager.Core.Models;
using TaskManager.Core.Services.Implementations;

namespace TaskManager.Console;

internal static class Program
{
    public static void Main(string[] args)
    {
        var issueDatabase = new IssueDatabase();
        var userDatabase = new UserDatabase();

        User.Database = userDatabase;
        Issue.Database = issueDatabase;

        var createUserCommand = new CreateUserCommand("Nikoloz Taturashvili", "tnicko@proton.me", "12345678");
        createUserCommand.Execute();
        
        var user1 = User.Database.GetUserByEmail("tnicko@proton.me");
        
        var issueId = Guid.NewGuid();
        var createIssueCommand = new CreateIssueCommand("Test Issue", "This is a test issue", issueId, Priority.High, null, null, user1);
        createIssueCommand.Execute();
        
        
        var updateIssueCommand = new UpdateIssueCommand("Test Issue", "This is a test issue", issueId, Priority.High, null, null, user1, Status.InProgress, issueDatabase.GetIssueById(issueId)?.CreatedAt);
        updateIssueCommand.Execute();
        
        updateIssueCommand = new UpdateIssueCommand("Test Issue", "This is a test issue", issueId, Priority.High, null, null, user1, Status.Done, issueDatabase.GetIssueById(issueId)?.CreatedAt);
        updateIssueCommand.Execute();
        foreach (var issue in issueDatabase.GetAllIssues())
        {
            System.Console.WriteLine($"Title: {issue?.Title}\nDescription: {issue?.Description}\nPriority: {issue?.Priority}\nStatus: {issue?.Status}\nCreated At: {issue?.CreatedAt}\nDue: {issue?.Due}\nFinished At: {issue?.FinishedAt}\nAssignee: {issue?.Assignee?.Name}");
        }
    }
}
