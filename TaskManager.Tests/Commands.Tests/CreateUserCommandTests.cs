using FluentAssertions;
using TaskManager.Core.Commands;
using TaskManager.Core.Models;

namespace TaskManager.Tests.Commands.Tests;

public class CreateUserCommandTests
{
    private readonly string _name = "Test";
    private readonly string _email = "test@test.com";
    private readonly string _password = "12345678";
    
    [Fact]
    public void CreateUserCommand_Execute_ShouldAddUserToDatabase()
    {
        // Arrange
        var command = new CreateUserCommand(_name, _email, _password);
        var executionTime = DateTime.Now;
        
        // Act
        Action action = () => command.Execute();
        var result = User.Database?.GetUserByEmail(_email);
        
        // Assert
        action.Should().NotThrow();
        result?.Name.Should().Be(_name);
        result?.Email.Should().Be(_email);
        result?.Password.Should().Be(_password);
        result?.CreatedAt.Should().Be(executionTime);
    }
    
    [Fact]
    public void CreateUserCommand_Execute_ShouldThrowArgumentException_WhenNameIsLessThanOneCharacter()
    {
        // Arrange
        var command = new CreateUserCommand("", _email, _password);
        
        // Act
        Action action = () => command.Execute();
        
        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("Name must be between 1 and 100 characters.");
    }
    
    [Fact]
    public void CreateUserCommand_Execute_ShouldThrowArgumentException_WhenEmailIsInvalid()
    {
        // Arrange
        var command = new CreateUserCommand(_name, "test", _password);
        
        // Act
        Action action = () => command.Execute();
        
        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("Email is not valid.");
    }
    
    [Fact]
    public void CreateUserCommand_Execute_ShouldThrowArgumentException_WhenPasswordIsLessThanEightCharacters()
    {
        // Arrange
        var command = new CreateUserCommand(_name, _email, "1234567");
        
        // Act
        Action action = () => command.Execute();
        
        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("Password must be between 8 and 16 characters.");
    }
    
    [Fact]
    public void CreateUserCommand_Execute_ShouldThrowArgumentException_WhenNameIsMoreThanOneHundredCharacters()
    {
        // Arrange
        var name = new string('a', 101);
        var command = new CreateUserCommand(name, _email, _password);
        
        // Act
        Action action = () => command.Execute();
        
        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("Name must be between 1 and 100 characters.");
    }
    
    [Fact]
    public void CreateUserCommand_Execute_ShouldThrowArgumentException_WhenPasswordIsMoreThanSixteenCharacters()
    {
        // Arrange
        var password = new string('a', 17);
        var command = new CreateUserCommand(_name, _email, password);
        
        // Act
        Action action = () => command.Execute();
        
        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("Password must be between 8 and 16 characters.");
    }
    
    [Fact]
    public void CreateUserCommand_Execute_ShouldThrowArgumentException_WhenEmailIsMoreThanOneHundredCharacters()
    {
        // Arrange
        var email = new string('a', 101) + "@test.com";
        var command = new CreateUserCommand(_name, email, _password);
        
        // Act
        Action action = () => command.Execute();
        
        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("Email must be between 1 and 100 characters.");
    }
    
    [Fact]
    public void CreateUserCommand_Execute_ShouldThrowArgumentException_WhenEmailIsLessThanOneCharacter()
    {
        // Arrange
        var command = new CreateUserCommand(_name, "", _password);
        
        // Act
        Action action = () => command.Execute();
        
        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("Email must be between 1 and 100 characters.");
    }
}