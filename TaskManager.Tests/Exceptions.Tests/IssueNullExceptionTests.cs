using FluentAssertions;
using TaskManager.Core.Exceptions;

namespace TaskManager.Tests.Exceptions.Tests;

public class IssueNullExceptionTests
{
    [Fact]
    public void Exceptions_IssueNullException_ShouldReturnMessage()
    {
        // Arrange
        var exception = new IssueNullException();
        
        // Act
        var result = exception.Message;
        
        // Assert
        result.Should().Be("Issue is null.");
    }
}