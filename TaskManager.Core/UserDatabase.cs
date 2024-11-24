namespace TaskManager.Core;

public class UserDatabase
{
    private readonly List<User> _users = [];

    public void AddUser(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        if (IsEmailRegistered(user.GetEmail()))
            throw new ArgumentException("Email is already registered.");

        _users.Add(user);
    }

    public void RemoveUser(string email)
    {
        var user = GetUserByEmail(email);
        if (user != null)
        {
            _users.Remove(user);
        }
    }

    private bool IsEmailRegistered(string email)
    {
        return _users.Any(u => u.GetEmail().Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    private User? GetUserByEmail(string email)
    {
        return _users.FirstOrDefault(u => 
            u.GetEmail().Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    private User? GetUserByCredentials(string email, string password)
    {
        return _users.FirstOrDefault(u => 
            u.GetEmail().Equals(email, StringComparison.OrdinalIgnoreCase) && 
            u.IsPasswordCorrect(password));
    }

    public bool UpdateUserEmail(string currentEmail, string newEmail)
    {
        if (IsEmailRegistered(newEmail))
            throw new ArgumentException("New email is already registered.");

        var user = GetUserByEmail(currentEmail);
        if (user == null) return false;
        user.ChangeEmail(newEmail);
        return true;
    }

    public bool UpdateUserPassword(string email, string currentPassword, string newPassword)
    {
        var user = GetUserByCredentials(email, currentPassword);
        if (user == null) return false;
        user.ChangePassword(newPassword);
        return true;
    }

    public bool UpdateUserName(string email, string newName)
    {
        var user = GetUserByEmail(email);
        if (user == null) return false;
        user.ChangeName(newName);
        return true;
    }

    public List<User> GetAllUsers()
    {
        return _users.ToList();
    }

    public int GetUserCount()
    {
        return _users.Count;
    }
}