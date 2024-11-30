namespace TaskManager.Core.Exceptions;

public class IssueNotFoundException : Exception
{
    public IssueNotFoundException() : base("Issue not found.") { }
    
    public IssueNotFoundException(string message) : base(message) { }
    
    public IssueNotFoundException(string message, Exception innerException) : base(message, innerException) { }
}