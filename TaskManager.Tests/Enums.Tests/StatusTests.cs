using TaskManager.Core.Enums;
using FluentAssertions;

namespace TaskManager.Tests.Enums.Tests;

public class StatusTests
{
    [Fact]
    public void Status_ToDo_ShouldReturnToDo()
    {
        // Arrange
        const Status status = Status.Todo;

        // Act
        var result = status.ToString();

        // Assert
        status.Should().Be(Status.Todo);
    }
    
    [Fact]
    public void Status_InProgress_ShouldReturnInProgress()
    {
        // Arrange
        const Status status = Status.InProgress;

        // Act
        var result = status.ToString();

        // Assert
        status.Should().Be(Status.InProgress);
    }

    [Fact]
    public void Status_Done_ShouldReturnDone()
    {
        // Arrange
        const Status status = Status.Done;

        // Act
        var result = status.ToString();

        // Assert
        status.Should().Be(Status.Done);
    }
}