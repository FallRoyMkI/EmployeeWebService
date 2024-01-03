namespace EmployeeWebService.Models.ResponseModels;

public class ExceptionResponseModel
{
    public int Code { get; set; }
    public string Message { get; set; }

    public ExceptionResponseModel(string message, int code)
    {
        Message = message;
        Code = code;
    }
}