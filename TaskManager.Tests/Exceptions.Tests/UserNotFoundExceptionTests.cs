using FluentAssertions;
using TaskManager.Core.Exceptions;

namespace TaskManager.Tests.Exceptions.Tests;

public class UserNotFoundExceptionTests
{
    [Fact]
    public void Exceptions_UserNotFoundException_ShouldReturnMessage() {
        // Arrange
        var exception = new UserNotFoundException();
        
        // Act
        var result = exception.Message;
        
        // Assert
        result.Should().Be("User not found");
    }
}