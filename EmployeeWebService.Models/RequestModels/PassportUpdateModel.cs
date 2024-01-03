namespace EmployeeWebService.Models.RequestModels;

public class PassportUpdateModel
{
    public int Id { get; set; }
    public string? Type { get; set; }
    public string? Number { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is PassportUpdateModel model &&
               model.Id == Id &&
               model.Type == Type &&
               model.Number == Number;
    }
}