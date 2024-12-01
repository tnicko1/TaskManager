using FluentAssertions;
using TaskManager.Core.Commands;
using TaskManager.Core.Enums;
using TaskManager.Core.Models;
using TaskManager.Core.Services.Implementations;

namespace TaskManager.Tests.Commands.Tests;

public class DeleteIssueCommandTests
{
    [Fact]
    public void Execute_WhenIssueDatabaseIsNull_ShouldThrowInvalidOperationException()
    {
        // Arrange
        Issue.Database = null;
        var command = new DeleteIssueCommand(Guid.NewGuid());
        
        // Act
        Action act = () => command.Execute();
        
        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Issue database is not initialized.");
    }
    
    [Fact]
    public void Execute_WhenIssueNotFound_ShouldThrowArgumentException()
    {
        // Arrange
        Issue.Database = new IssueDatabase();
        var command = new DeleteIssueCommand(Guid.NewGuid());
        
        // Act
        Action act = () => command.Execute();
        
        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Issue not found.");
    }
    
    [Fact]
    public void Execute_WhenIssueExists_ShouldDeleteIssue()
    {
        // Arrange
        var issue = new Issue
        {
            Id = Guid.NewGuid(),
            Title = "Test Issue",
            Priority = Priority.Low
        };
        Issue.Database = new IssueDatabase();
        Issue.Database.AddIssue(issue);
        var command = new DeleteIssueCommand(issue.Id);
        
        // Act
        command.Execute();
        
        // Assert
        Issue.Database.GetIssueById(issue.Id).Should().BeNull();
    }
    
    [Fact]
    public void Execute_WhenIssueExists_ShouldDeleteCorrectIssue()
    {
        // Arrange
        var issue1 = new Issue
        {
            Id = Guid.NewGuid(),
            Title = "Test Issue 1",
            Priority = Priority.Low
        };
        var issue2 = new Issue
        {
            Id = Guid.NewGuid(),
            Title = "Test Issue 2",
            Priority = Priority.Low
        };
        Issue.Database = new IssueDatabase();
        Issue.Database.AddIssue(issue1);
        Issue.Database.AddIssue(issue2);
        var command = new DeleteIssueCommand(issue1.Id);
        
        // Act
        command.Execute();
        
        // Assert
        Issue.Database.GetIssueById(issue1.Id).Should().BeNull();
        Issue.Database.GetIssueById(issue2.Id).Should().NotBeNull();
    }
}