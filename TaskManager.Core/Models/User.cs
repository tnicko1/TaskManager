using System.Text.RegularExpressions;
using TaskManager.Core.Services.Implementations;

namespace TaskManager.Core.Models;

public partial class User
{
    public required string? Name { get; set; }
    public required string? Email { get; set; }
    public required string? Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid Id { get; set; }
    public static UserDatabase? Database { get; set; }
    
    public User()
    {
        Database?.AddUser(this);
    }
}
