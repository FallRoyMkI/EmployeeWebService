﻿using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.Models;

public class EmployeeUpdateModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public int CompanyId { get; set; }
    public string PassportType { get; set; }
    public string PassportNumber { get; set; }
    public string DepartmentName { get; set; }
    public string DepartmentPhone { get; set; }
}