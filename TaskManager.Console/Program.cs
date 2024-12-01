using TaskManager.Console.LoadMethods;
using TaskManager.Console.Menu;
using TaskManager.Console.SaveMethods;

namespace TaskManager.Console;

internal static class Program
{
    public static void Main(string[] args)
    {
        LoadUsers.LoadUsersFromJson();
        LoadIssues.LoadIssuesFromJson();
        
        MainMenu.Show();

        SaveUsers.SaveUsersToJson();
        SaveIssues.SaveIssuesToJson();
    }
}
