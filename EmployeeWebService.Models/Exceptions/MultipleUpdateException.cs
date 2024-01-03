namespace EmployeeWebService.Models.Exceptions;

public class MultipleUpdateException : Exception
{
    public MultipleUpdateException(string message) : base(message) { }
}