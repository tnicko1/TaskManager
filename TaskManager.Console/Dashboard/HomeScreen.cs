using TaskManager.Console.Menu;

namespace TaskManager.Console.Dashboard;

public static class HomeScreen
{
    public static void Show()
    {
        while (true)
        {
            System.Console.WriteLine("Welcome to the Task Manager!");
            System.Console.WriteLine("1. View Issues");
            System.Console.WriteLine("2. Add Issue");
            System.Console.WriteLine("3. Edit Issue");
            System.Console.WriteLine("4. Delete Issue");
            System.Console.WriteLine("5. Logout");
            System.Console.WriteLine("Choose an option: ");
            var option = Convert.ToInt32(System.Console.ReadLine());

            switch (option)
            {
                case 1:
                    IssueMenu.ViewIssues();
                    Show();
                    break;
                case 2:
                    IssueMenu.AddIssue();
                    Show();
                    break;
                case 3:
                    IssueMenu.EditIssue();
                    Show();
                    break;
                case 4:
                    IssueMenu.DeleteIssue();
                    Show();
                    break;
                case 5:
                    MainMenu.Show();
                    break;
                default:
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("Invalid option.");
                    System.Console.ResetColor();
                    continue;
            }

            break;
        }
    }
}