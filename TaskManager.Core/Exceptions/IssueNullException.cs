namespace TaskManager.Core.Exceptions;

public class IssueNullException : Exception
{
    public IssueNullException() : base("Issue is null.") { }
    
    public IssueNullException(string message) : base(message) { }
    
    public IssueNullException(string message, Exception innerException) : base(message, innerException) { }
}