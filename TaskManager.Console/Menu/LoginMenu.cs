using TaskManager.Core.Models;

namespace TaskManager.Console.Menu;

public static class LoginMenu
{
    public static void Show()
    {
        System.Console.WriteLine("Please login to continue.");
        System.Console.WriteLine("Email: ");
        var email = System.Console.ReadLine();
        System.Console.WriteLine("Password: ");
        var password = System.Console.ReadLine();

        if (email == null) return;
        var user = User.Database?.GetUserByEmail(email);
        if (user == null)
        {
            System.Console.ForegroundColor = System.ConsoleColor.Red;
            System.Console.WriteLine("User not found.");
            System.Console.ResetColor();
            return;
        }

        if (user.Password != password)
        {
            System.Console.ForegroundColor = System.ConsoleColor.Red;
            System.Console.WriteLine("Invalid password.");
            System.Console.ResetColor();
            return;
        }

        System.Console.ForegroundColor = System.ConsoleColor.Green;
        System.Console.WriteLine($"Hello, {user.Name}!");
        System.Console.ResetColor();
        
        Dashboard.HomeScreen.Show();
    }
}