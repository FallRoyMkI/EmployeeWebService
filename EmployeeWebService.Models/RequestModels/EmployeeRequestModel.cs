namespace EmployeeWebService.Models.ViewModels;

public class EmployeeRequestModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public int CompanyId { get; set; }
    public int PassportId { get; set; }
    public int DepartmentId { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is EmployeeRequestModel model &&
               model.Name == Name &&
               model.Surname == Surname &&
               model.Phone == Phone &&
               model.CompanyId == CompanyId &&
               model.PassportId == PassportId &&
               model.DepartmentId == DepartmentId;
    }
}