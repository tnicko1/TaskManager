using FluentAssertions;
using TaskManager.Core.Exceptions;

namespace TaskManager.Tests.Exceptions.Tests;

public class InvalidStatusChangeExceptionTests
{
    [Fact]
    public void Exceptions_InvalidStatusChangeException_ShouldReturnMessage()
    {
        // Arrange
        var exception = new InvalidStatusChangeException();
        
        // Act
        var result = exception.Message;
        
        // Assert
        result.Should().Be("Invalid status change.");
    }
}