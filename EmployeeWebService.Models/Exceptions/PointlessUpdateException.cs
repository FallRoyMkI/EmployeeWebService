namespace EmployeeWebService.Models.Exceptions;

public class PointlessUpdateException : Exception
{
    public PointlessUpdateException(string message) : base(message) { }
}