using FluentAssertions;
using TaskManager.Core.Exceptions;

namespace TaskManager.Tests.Exceptions.Tests;

public class IssueNotFoundExceptionTest
{
    [Fact]
    public void Exceptions_IssueNotFoundException_ShouldReturnMessage()
    {
        // Arrange
        var exception = new IssueNotFoundException();
        
        // Act
        var result = exception.Message;
        
        // Assert
        result.Should().Be("Issue not found.");
    }
}