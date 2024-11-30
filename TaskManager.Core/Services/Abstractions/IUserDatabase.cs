namespace TaskManager.Core.Services.Abstractions;

public interface IUserDatabase
{
    void AddUser(Models.User? user);
    Models.User? GetUserById(Guid userId);
    Models.User? GetUserByEmail(string email);
    void UpdateUser(Models.User user);
    void DeleteUser(Guid userId);
    IEnumerable<Models.User?> GetAllUsers();
}