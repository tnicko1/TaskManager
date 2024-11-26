namespace TaskManager.Core;
using System.Text.RegularExpressions;

public partial class User
{
    private string Name { get; set; }
    private string Email { get; set; }
    private string Password { get; set; }
    private DateTime CreatedAt { get; set; }

    private User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
        CreatedAt = DateTime.Now;
    }
        
    public static User? CreateUser(string name, string email, string password, UserDatabase? database = null)
    {
        try
        {
            if (ValidateName(name) && ValidateEmail(email) && ValidatePassword(password))
            {
                var user = new User(name, email, password);
                database?.AddUser(user);

                const string usersFile = "users.txt";
                var allLines = File.ReadAllLines(usersFile);
                if (Array.FindIndex(allLines, line => line.Contains(email)) != -1) return user;
                var toWrite = $"{user.Name},{user.Email},{user.Password},{user.CreatedAt}";
                File.AppendAllText(usersFile, toWrite + "\n");

                return user;
            }
        }
        catch (ArgumentException e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(e.Message);
            Console.ResetColor();
        }

        return null;
    }
    
    private static bool ValidateName(string name)
    {
        if (name.Length is > 1 and <= 100)
        {
            return true;
        }
        throw new ArgumentException("Name must be between 1 and 100 characters.");
        
    }
    
    private static bool ValidateEmail(string email)
    {
        if (email.Length is <= 1 or > 100) throw new ArgumentException("Email must be between 1 and 100 characters.");
        if (MyRegex().IsMatch(email))
        {
            return true;
        }

        throw new ArgumentException("Email is not valid.");
    }
    
    private static bool ValidatePassword(string password)
    {
        if (password.Length is >= 8 and <= 16)
        {
            return true;
        }
        throw new ArgumentException("Password must be between 8 and 16 characters.");
    }
    
    public void UpdateLastLogin()
    {
        
    }
    
    public void ChangeName(string newName)
    {
        try
        {
            if (!ValidateName(newName)) return;
            const string usersFile = "users.txt";
            var lines = File.ReadAllLines(usersFile);
            var userIndex = Array.FindIndex(lines, line => line.Contains(Name));
            if (userIndex == -1) return;
            Name = newName;
            lines[userIndex] = $"{Name},{Email},{Password},{CreatedAt}";
            File.WriteAllLines(usersFile, lines);
        } catch (ArgumentException e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(e.Message);
            Console.ResetColor();
        }
    }
    
    public void ChangeEmail(string newEmail)
    {
        try
        {
            if (!ValidateEmail(newEmail)) return;
            const string usersFile = "users.txt";
            var lines = File.ReadAllLines(usersFile);
            var userIndex = Array.FindIndex(lines, line => line.Contains(Email));
            if (userIndex == -1) return;
            Email = newEmail;
            lines[userIndex] = $"{Name},{Email},{Password},{CreatedAt}";
            File.WriteAllLines(usersFile, lines);
        } catch (ArgumentException e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(e.Message);
            Console.ResetColor();
        }
    }
    
    public void ChangePassword(string newPassword)
    {
        try
        {
            if (!ValidatePassword(newPassword)) return;
            const string usersFile = "users.txt";
            var lines = File.ReadAllLines(usersFile);
            var userIndex = Array.FindIndex(lines, line => line.Contains(Password));
            if (userIndex == -1) return;
            Password = newPassword;
            lines[userIndex] = $"{Name},{Email},{Password},{CreatedAt}";
            File.WriteAllLines(usersFile, lines);
        } catch (ArgumentException e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(e.Message);
            Console.ResetColor();
        }
    }
    
    public string GetName()
    {
        return Name;
    }
    
    public string GetEmail()
    {
        return Email;
    }
    
    public string GetPassword()
    {
        return Password;
    }
    
    public bool IsPasswordCorrect(string password)
    {
        return Password == password;
    }
    
    public bool IsEmailCorrect(string email)
    {
        return Email.Equals(email, StringComparison.OrdinalIgnoreCase);
    }
    
    public bool IsNameCorrect(string name)
    {
        return Name == name;
    }
    
    public bool IsUserCorrect(string name, string email, string password)
    {
        return Name == name && IsEmailCorrect(email) && Password == password;
    }

    [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
    private static partial Regex MyRegex();
}
