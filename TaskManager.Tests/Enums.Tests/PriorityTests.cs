using TaskManager.Core.Enums;
using FluentAssertions;

namespace TaskManager.Tests.Enums.Tests;

public class PriorityTests
{
    [Fact]
    public void Priority_High_ShouldReturnHigh()
    {
        // Arrange
        const Priority priority = Priority.High;

        // Act
        var result = priority.ToString();

        // Assert
        priority.Should().Be(Priority.High);
    }
    
    [Fact]
    public void Priority_Medium_ShouldReturnMedium()
    {
        // Arrange
        const Priority priority = Priority.Medium;

        // Act
        var result = priority.ToString();

        // Assert
        priority.Should().Be(Priority.Medium);
    }

    [Fact]
    public void Priority_Low_ShouldReturnLow()
    {
        // Arrange
        const Priority priority = Priority.Low;

        // Act
        var result = priority.ToString();

        // Assert
        priority.Should().Be(Priority.Low);
    }
}