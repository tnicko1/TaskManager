namespace TaskManager.Core;

internal static class Program
{
    public static void Main(string[] args)
    {
        // Possible use case at the moment
        
        var userDatabase = new UserDatabase();
        var issueDatabase = new IssueDatabase();

        const string usersFile = "users.txt";
        var allLines = File.ReadAllLines(usersFile);
        foreach (var line in allLines)
        {
            var parts = line.Split(',');
            var name = parts[0];
            var email = parts[1];
            var password = parts[2];
            var createdAt = DateTime.Parse(parts[3]);
            User.CreateUser(name, email, password, userDatabase);
        }

        var users = userDatabase.GetAllUsers();
        foreach (var user in users)
        {
            Console.WriteLine(user.GetName());
        }
    }
}