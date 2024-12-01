using System.Text.Json;
using System.Text.Json.Serialization;
using TaskManager.Core.Models;

namespace TaskManager.Console.SaveMethods;

public static class SaveIssues
{
    public static void SaveIssuesToJson()
    {
        var issues = Issue.Database?.GetAllIssues();
        if (issues == null) return;
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };
        var issuesJson = JsonSerializer.Serialize(issues, options);
        File.WriteAllText(Path.Combine("..", "..", "..", "Jsons", "issues.json"), issuesJson);
    }
}