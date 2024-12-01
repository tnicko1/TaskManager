namespace TaskManager.Console.Menu;

public class MainMenu
{
    public static void Show()
    {
        while (true)
        {
            System.Console.WriteLine("Welcome to Task Manager!");
            System.Console.WriteLine("1. Login");
            System.Console.WriteLine("2. Register");
            System.Console.WriteLine("3. Exit");
            var choice = Convert.ToInt32(System.Console.ReadLine());
            switch (choice)
            {
                case 1:
                    LoginMenu.Show();
                    break;
                case 2:
                    RegisterMenu.Show();
                    break;
                case 3:
                    return;
                default:
                    System.Console.ForegroundColor = System.ConsoleColor.Red;
                    System.Console.WriteLine("Invalid choice.");
                    System.Console.ResetColor();
                    break;
            }
        }
    }
}