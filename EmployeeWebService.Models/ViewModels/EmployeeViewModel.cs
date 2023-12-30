using EmployeeWebService.Models.Entities;

namespace EmployeeWebService.Models.ViewModels;

public class EmployeeViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public int CompanyId { get; set; }
    public PassportViewModel Passport { get; set; }
    public DepartmentViewModel Department { get; set; }
}