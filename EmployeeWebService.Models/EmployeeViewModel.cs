using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.Models;

public class EmployeeViewModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public int CompanyId { get; set; }
    public PassportRequestModel Passport { get; set; }
    public DepartmentRequestModel Department { get; set; }
}