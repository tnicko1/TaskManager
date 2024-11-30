using FluentAssertions;
using TaskManager.Core.Commands;
using TaskManager.Core.Enums;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Models;
using TaskManager.Core.Services.Implementations;

namespace TaskManager.Tests.Commands.Tests;

public class UpdateIssueCommandTests
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
    public void UpdateIssueCommand_Execute_ShouldThrowArgumentException_WhenTitleIsLessThanOneCharacter()
    {
        // Arrange
        var command = new UpdateIssueCommand("", _description, _id, _priority, _due, _finishedAt, _assignee, Status.Todo, DateTime.Now);
        
        // Act
        Action action = () => command.Execute();
        
        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("Title must be between 1 and 100 characters.");
    }
    
    [Fact]
    public void UpdateIssueCommand_Execute_ShouldThrowArgumentException_WhenTitleIsMoreThanOneHundredCharacters()
    {
        // Arrange
        var command = new UpdateIssueCommand(new string('a', 101), _description, _id, _priority, _due, _finishedAt, _assignee, Status.Todo, DateTime.Now);
        
        // Act
        Action action = () => command.Execute();
        
        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("Title must be between 1 and 100 characters.");
    }
    
    [Fact]
    public void UpdateIssueCommand_Execute_ShouldThrowArgumentException_WhenDescriptionIsMoreThanOneThousandCharacters()
    {
        // Arrange
        var command = new UpdateIssueCommand(_title, new string('a', 4001), _id, _priority, _due, _finishedAt, _assignee, Status.Todo, DateTime.Now);
        
        // Act
        Action action = () => command.Execute();
        
        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("Description must be between 1 and 4000 characters.");
    }
    
    [Fact]
    public void UpdateIssueCommand_Execute_ShouldThrowArgumentException_WhenDueDateIsLessThanCurrentDate()
    {
        // Arrange
        var command = new UpdateIssueCommand(_title, _description, _id, _priority, DateTime.Now.AddDays(-1), _finishedAt, _assignee, Status.Todo, DateTime.Now);
        
        // Act
        Action action = () => command.Execute();
        
        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("Due date must be in the future.");
    }
    
    [Fact]
    public void UpdateIssueCommand_Execute_ShouldThrowArgumentException_WhenFinishedAtDateIsLessThanCurrentDate()
    {
        // Arrange
        var command = new UpdateIssueCommand(_title, _description, _id, _priority, _due, DateTime.Now.AddDays(-1), _assignee, Status.Todo, DateTime.Now);
        
        // Act
        Action action = () => command.Execute();
        
        // Assert
        action.Should().Throw<ArgumentException>().WithMessage("Finished at date must be greater than current date.");
    }
    
    [Fact]
    public void UpdateIssueCommand_Execute_ShouldThrowIssueNotFoundException_WhenIssueDoesNotExist()
    {
        // Arrange
        var command = new UpdateIssueCommand(_title, _description, Guid.NewGuid(), _priority, _due, _finishedAt, _assignee, Status.Todo, DateTime.Now);
        
        // Act
        Action action = () => command.Execute();
        
        // Assert
        action.Should().Throw<IssueNotFoundException>().WithMessage("Issue does not exist.");
    }
}