using TaskManager.Core.Commands;
using TaskManager.Core.Enums;
using TaskManager.Core.Models;

namespace TaskManager.Console.Dashboard;

public static class IssueMenu
{
    public static void ViewIssues()
    {
        if (Issue.Database == null) return;
        foreach (var issue in Issue.Database.GetAllIssues())
        {
            if (issue != null)
            {
                System.Console.WriteLine($"Title: {issue.Title}");
                System.Console.WriteLine($"Description: {issue.Description}");
                System.Console.WriteLine($"Status: {issue.Status}");
                System.Console.WriteLine($"Priority: {issue.Priority}");
                System.Console.WriteLine($"Due: {(issue.Due != null ? issue.Due?.ToString("yyyy-MM-dd HH:mm:ss") : "No due date")}");
                System.Console.WriteLine($"Assignee: {issue.Assignee?.Name ?? "None"}");
                System.Console.WriteLine($"Created At: {issue.CreatedAt?.ToString("yyyy-MM-dd HH:mm:ss")}");
                System.Console.WriteLine($"Finished At: {issue.FinishedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? "Not finished yet"}");
                System.Console.WriteLine($"ID: {issue.Id}");
            }

            System.Console.WriteLine();
        }
    }
    
    public static void AddIssue()
    {
        while (true) {
            System.Console.WriteLine("Title: ");
            var title = System.Console.ReadLine();
            System.Console.WriteLine("Description: ");
            var description = System.Console.ReadLine();
            System.Console.WriteLine("Priority: ");
            System.Console.WriteLine("1. Low\n2. Medium\n3. High");
            var priority = Convert.ToInt32(System.Console.ReadLine());
            System.Console.WriteLine("Due (after how many days is the deadline, write an integer): ");
            var due = Convert.ToInt32(System.Console.ReadLine());
            System.Console.WriteLine("Assignee email (none if no assignee): ");
            var assignee = System.Console.ReadLine();
            
            if (title == null || description == null || assignee == null) return;
            
            var priorityValue = Priority.Low;
            priorityValue = priority switch
            {
                1 => Priority.Low,
                2 => Priority.Medium,
                3 => Priority.High,
                _ => priorityValue
            };
            
            var dueValue = DateTime.Now.AddDays(due);
            
            var assigneeValue = assignee == "none" ? null : User.Database?.GetUserByEmail(assignee);

            var createIssueCommand = new CreateIssueCommand(title, description, Guid.NewGuid(), priorityValue, dueValue, finishedAt: null, assigneeValue);
            try
            {
                createIssueCommand.Execute();
            }
            catch (Exception e)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine("Please try again.");
                System.Console.ResetColor();
                continue;
            }

            System.Console.WriteLine("Do you want to add another issue? (y/n)");
            var option = System.Console.ReadKey().KeyChar.ToString();
            if (option == "n") break;
        }
    }
    
    public static void EditIssue()
    {
        System.Console.WriteLine("Enter the ID of the issue you want to edit: ");
        var id = Guid.Parse(System.Console.ReadLine() ?? string.Empty);
        var issue = Issue.Database?.GetIssueById(id);
        
        if (issue == null)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("Issue not found.");
            System.Console.ResetColor();
            return;
        }
        
        System.Console.WriteLine("Title: ");
        var title = System.Console.ReadLine();
        System.Console.WriteLine("Description: ");
        var description = System.Console.ReadLine();
        System.Console.WriteLine("Priority: ");
        System.Console.WriteLine("1. Low\n2. Medium\n3. High");
        var priority = Convert.ToInt32(System.Console.ReadLine());
        System.Console.WriteLine("Due (after how many days is the deadline, write an integer): ");
        var due = Convert.ToInt32(System.Console.ReadLine());
        System.Console.WriteLine("Assignee email (none if no assignee): ");
        var assignee = System.Console.ReadLine();
            
        if (title == null || description == null || assignee == null) return;
            
        var priorityValue = Priority.Low;
        priorityValue = priority switch
        {
            1 => Priority.Low,
            2 => Priority.Medium,
            3 => Priority.High,
            _ => priorityValue
        };
            
        var dueValue = DateTime.Now.AddDays(due);
            
        var assigneeValue = assignee == "none" ? null : User.Database?.GetUserByEmail(assignee);
            
        var updateIssueCommand = new UpdateIssueCommand(title, description, id, priorityValue, dueValue, finishedAt: null, assigneeValue, issue.Status, issue.CreatedAt);
        try
        {
            updateIssueCommand.Execute();
        }
        catch (Exception e)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(e.Message);
            System.Console.ResetColor();
        }
    }
    
    public static void DeleteIssue()
    {
        System.Console.WriteLine("Enter the ID of the issue you want to delete: ");
        var id = Guid.Parse(System.Console.ReadLine() ?? string.Empty);
        var deleteIssueCommand = new DeleteIssueCommand(id);
        try
        {
            deleteIssueCommand.Execute();
        }
        catch (Exception e)
        {
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(e.Message);
            System.Console.ResetColor();
        }
    }
}