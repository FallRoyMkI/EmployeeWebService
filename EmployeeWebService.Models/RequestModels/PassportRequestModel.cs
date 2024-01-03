namespace EmployeeWebService.Models.ViewModels;

public class PassportRequestModel
{
    public string Type { get; set; }
    public string Number { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is PassportRequestModel model &&
               model.Type == Type &&
               model.Number == Number;
    }
}