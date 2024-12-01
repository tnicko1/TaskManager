using System.Text.RegularExpressions;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Models;
using TaskManager.Core.Services.Implementations;

namespace TaskManager.Core.Commands;

public partial class CreateUserCommand(string name, string email, string password)
{
    private string Name { get; set; } = name;
    private string Email { get; set; } = email;
    private string Password { get; set; } = password;

    public void Execute()
    {
        Validate();
        var user = new User
        {
            Name = Name,
            Email = Email,
            Password = Password,
            CreatedAt = DateTime.Now,
            Id = Guid.NewGuid()
        };
    }

    private void Validate()
    {
        if (Name.Length is < 1 or > 100)
        {
            throw new ArgumentException("Name must be between 1 and 100 characters.");
        }
        
        if (Email.Length is < 1 or > 100)
        {
            throw new ArgumentException("Email must be between 1 and 100 characters.");
        }
        
        if (!MyRegex().IsMatch(Email))
        {
            throw new ArgumentException("Email is not valid.");
        }
        
        if (Password.Length is < 8 or > 16)
        {
            throw new ArgumentException("Password must be between 8 and 16 characters.");
        }
        
        if (User.Database?.GetUserByEmail(Email) != null)
        {
            throw new ArgumentException("A user with this email already exists.");
        }
    }

    private static Regex MyRegex()
    {
        return MyRegex1();
    }

    [GeneratedRegex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
    private static partial Regex MyRegex1();
}