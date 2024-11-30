namespace TaskManager.Core.Exceptions;

public class UserNullException : Exception
{
    public UserNullException() : base("User is null.") { }
    
    public UserNullException(string message) : base(message) { }
    
    public UserNullException(string message, Exception innerException) : base(message, innerException) { }
}