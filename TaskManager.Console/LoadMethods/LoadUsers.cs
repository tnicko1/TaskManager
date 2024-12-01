using System.Text.Json;
using TaskManager.Core.Models;
using TaskManager.Core.Services.Implementations;

namespace TaskManager.Console.LoadMethods;

public static class LoadUsers
{
    public static void LoadUsersFromJson()
    {
        User.Database = new UserDatabase();
        
        var usersJson = File.ReadAllText(Path.Combine("..", "..", "..", "Jsons", "users.json"));
        if (string.IsNullOrWhiteSpace(usersJson)) return;
        var users = JsonSerializer.Deserialize<List<User>>(usersJson);
        if (users == null) return;
        foreach (var user in users)
        {
            if (user.Email != null && User.Database?.GetUserByEmail(user.Email) == null)
            {
                User.Database?.AddUser(user);
            }
        }
    }
}