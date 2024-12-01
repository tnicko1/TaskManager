using System.Text.Json;
using TaskManager.Core.Models;

namespace TaskManager.Console.SaveMethods;

public static class SaveUsers
{
    public static void SaveUsersToJson()
    {
        var users = User.Database?.GetAllUsers();
        if (users == null) return;
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        var usersJson = JsonSerializer.Serialize(users, options);
        File.WriteAllText(Path.Combine("..", "..", "..", "Jsons", "users.json"), usersJson);
    }
}