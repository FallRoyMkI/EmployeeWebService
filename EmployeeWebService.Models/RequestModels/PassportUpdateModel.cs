namespace EmployeeWebService.Models.RequestModels;

public class PassportUpdateModel
{
    public int Id { get; set; }
    public string? Type { get; set; }
    public string? Number { get; set; }
}