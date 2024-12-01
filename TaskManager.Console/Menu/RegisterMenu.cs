using TaskManager.Core.Commands;

namespace TaskManager.Console.Menu;

public static class RegisterMenu
{
    public static void Show()
    {
        System.Console.WriteLine("Please register to continue.");
        System.Console.WriteLine("Name: ");
        var name = System.Console.ReadLine();
        System.Console.WriteLine("Email: ");
        var email = System.Console.ReadLine();
        System.Console.WriteLine("Password: ");
        var password = System.Console.ReadLine();

        if (name == null || email == null || password == null) return;
        var createUserCommand = new CreateUserCommand(name, email, password);

        try
        {
            createUserCommand.Execute();
        } catch (System.ArgumentException e)
        {
            System.Console.ForegroundColor = System.ConsoleColor.Red;
            System.Console.WriteLine(e.Message);
            System.Console.ResetColor();
            return;
        }

        System.Console.ForegroundColor = System.ConsoleColor.Green;
        System.Console.WriteLine("User created successfully.");
        System.Console.ResetColor();
    }
}