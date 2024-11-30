using FluentAssertions;
using TaskManager.Core.Commands;
using TaskManager.Core.Enums;
using TaskManager.Core.Models;
using TaskManager.Core.Services.Implementations;

namespace TaskManager.Tests.Commands.Tests;

public class CreateIssueCommandTests
{
    private readonly string _title = "Test";
    private readonly string _description = "Test";
    private readonly Guid _id = Guid.NewGuid();
    private readonly Priority _priority = Priority.High;
    private readonly DateTime _due = DateTime.Now.AddDays(1);
    private readonly DateTime _finishedAt = DateTime.Now.AddDays(2);
    private readonly User _assignee = new()
    {
        Id = Guid.NewGuid(),
        Name = "Test",
        Email = "test@test.com",
        Password = "Test",
    };
    
    
    [Fact]
    public void CreateIssueCommand_Execute_ShouldAddIssueToDatabase()
    {
        // Arrange
        var command = new CreateIssueCommand(_title, _description, _id, _priority, _due, _finishedAt, _assignee);
        var executionTime = DateTime.Now;
        
        // Act
        Action action = () => command.Execute();
        var result = Issue.Database?.GetIssueById(_id);
            
        // Assert
        action.Should().NotThrow();
        result?.Title.Should().Be(_title);
        result?.Description.Should().Be(_description);
        result?.Id.Should().Be(_id);
        result?.Priority.Should().Be(_priority);
        result?.Status.Should().Be(Status.Todo);
        result?.CreatedAt.Should().Be(executionTime);
        result?.Due.Should().Be(_due);
        result?.FinishedAt.Should().Be(_finishedAt);
        result?.Assignee.Should().Be(_assignee);
    }

    [Fact]
    public void CreateIssueCommand_Execute_ShouldThrowArgumentException_WhenTitleIsLessThanOneCharacter()
    {
        // Arrange
        var command = new CreateIssueCommand("", _description, _id, _priority, _due, _finishedAt, _assignee);
      
        // Act
        Action action = () => command.Execute();
        
        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("Title must be between 1 and 100 characters.");
    }

    [Fact]
    public void CreateIssueCommand_Execute_ShouldThrowArgumentException_WhenTitleIsMoreThanOneHundredCharacters()
    {
        // Arrange
        var title = new string('a', 101);
        var command = new CreateIssueCommand(title, _description, _id, _priority, _due, _finishedAt, _assignee);
        
        // Act
        Action action = () => command.Execute();
        
        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("Title must be between 1 and 100 characters.");
    }

    [Fact]
    public void CreateIssueCommand_Execute_ShouldThrowArgumentException_WhenDescriptionIsLessThanOneCharacter()
    {
        // Arrange
        var command = new CreateIssueCommand(_title, "", _id, _priority, _due, _finishedAt, _assignee);
        
        // Act
        Action action = () => command.Execute();
        
        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("Description must be between 1 and 4000 characters.");
    }

    [Fact]
    public void CreateIssueCommand_Execute_ShouldThrowArgumentException_WhenDescriptionIsMoreThanFourThousandCharacters()
    {
        // Arrange
        var description = new string('a', 4001);
        var command = new CreateIssueCommand(_title, description, _id, _priority, _due, _finishedAt, _assignee);
        
        // Act
        Action action = () => command.Execute();
        
        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("Description must be between 1 and 4000 characters.");
    }

    [Fact]
    public void CreateIssueCommand_Execute_ShouldThrowArgumentException_WhenDueDateIsNotInTheFuture()
    {
        // Arrange
        var due = DateTime.Now;
        var command = new CreateIssueCommand(_title, _description, _id, _priority, due, _finishedAt, _assignee);
        
        // Act
        Action action = () => command.Execute();
        
        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("Due date must be in the future.");
    }
    
    [Fact]
    public void CreateIssueCommand_Execute_ShouldThrowArgumentException_WhenIssueAlreadyExists()
    {
        // Arrange
        var command = new CreateIssueCommand(_title, _description, _id, _priority, _due, _finishedAt, _assignee);
        Issue.Database = new IssueDatabase();
        Issue.Database.AddIssue(new Issue
        {
            Title = _title,
            Description = _description,
            Id = _id,
            Priority = _priority,
            Status = Status.Todo,
            CreatedAt = DateTime.Now,
            Due = _due,
            FinishedAt = _finishedAt,
            Assignee = _assignee
        });
        
        // Act
        Action action = () => command.Execute();
        
        // Assert
        action.Should().Throw<ArgumentException>();
    }
}