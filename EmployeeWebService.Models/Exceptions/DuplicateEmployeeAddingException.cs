namespace EmployeeWebService.Models.Exceptions;

public class DuplicateEmployeeAddingException : Exception
{
    public DuplicateEmployeeAddingException(string message) : base(message) { }
}