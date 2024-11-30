namespace TaskManager.Core.Exceptions;

public class InvalidStatusChangeException : Exception
{
    public InvalidStatusChangeException() : base("Invalid status change.") { }
    
    public InvalidStatusChangeException(string message) : base(message) { }
    
    public InvalidStatusChangeException(string message, Exception innerException) : base(message, innerException) { }
}