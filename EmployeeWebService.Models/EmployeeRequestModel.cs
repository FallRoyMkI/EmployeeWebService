using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.Models;

public class EmployeeRequestModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public int CompanyId { get; set; }
    public PassportViewModel Passport { get; set; }
    public DepartmentViewModel Department { get; set; }
}