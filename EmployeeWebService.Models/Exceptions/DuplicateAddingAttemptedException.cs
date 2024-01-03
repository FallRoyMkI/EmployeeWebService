namespace EmployeeWebService.Models.Exceptions;

public class DuplicateAddingAttemptedException : Exception
{
    public DuplicateAddingAttemptedException(string message) : base(message) { }
}