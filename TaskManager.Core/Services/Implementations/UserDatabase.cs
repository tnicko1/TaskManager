using TaskManager.Core.Exceptions;
using TaskManager.Core.Services.Abstractions;

namespace TaskManager.Core.Services.Implementations;

public class UserDatabase : IUserDatabase
{
    private readonly List<Models.User?> _users = [];

    public void AddUser(Models.User? user)
    {
        _users.Add(user);
    }

    public Models.User? GetUserById(Guid userId)
    {
        return _users.FirstOrDefault(u => u?.Id == userId);
    }

    public Models.User? GetUserByEmail(string email)
    {
        return _users.FirstOrDefault(u => u?.Email == email);
    }

    public void UpdateUser(Models.User user)
    {
        var existingUser = _users.FirstOrDefault(u => u?.Id == user.Id);
        if (existingUser == null)
        {
            throw new UserNotFoundException();
        }

        existingUser.Name = user.Name;
        existingUser.Email = user.Email;
        existingUser.Password = user.Password;
    }

    public void DeleteUser(Guid userId)
    {
        var user = _users.FirstOrDefault(u => u?.Id == userId);
        if (user == null)
        {
            throw new UserNotFoundException();
        }

        _users.Remove(user);
    }

    public IEnumerable<Models.User?> GetAllUsers()
    {
        return _users;
    }
}