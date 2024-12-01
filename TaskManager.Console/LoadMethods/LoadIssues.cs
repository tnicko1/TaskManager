using System.Text.Json;
using System.Text.Json.Serialization;
using TaskManager.Core.Models;
using TaskManager.Core.Services.Implementations;

namespace TaskManager.Console.LoadMethods;

public static class LoadIssues
{
    public static void LoadIssuesFromJson()
    {
        Issue.Database = new IssueDatabase();
        
        var issuesJson = File.ReadAllText(Path.Combine("..", "..", "..", "Jsons", "issues.json"));
        if (string.IsNullOrWhiteSpace(issuesJson)) return;
        var options = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() }
        };
        var issues = JsonSerializer.Deserialize<List<Issue>>(issuesJson, options);
        if (issues == null) return;
        foreach (var issue in issues.Where(issue => issue.Id != Guid.Empty && Issue.Database?.GetIssueById(issue.Id) == null))
        {
            Issue.Database?.AddIssue(issue);
        }
    }
}