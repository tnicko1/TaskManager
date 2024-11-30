using FluentAssertions;
using TaskManager.Core.Exceptions;

namespace TaskManager.Tests.Exceptions.Tests;

public class UserNullExceptionTests
{
    [Fact]
    public void Exceptions_UserNullException_ShouldReturnMessage()
    {
        // Arrange
        var exception = new UserNullException();
        
        // Act
        var result = exception.Message;
        
        // Assert
        result.Should().Be("User is null.");
    }
}