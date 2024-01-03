﻿using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeWebService.Models.ViewModels;

public class EmployeeUpdateModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Phone { get; set; }
    public int? CompanyId { get; set; }
    public int? DepartmentId { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is EmployeeUpdateModel model &&
               model.Id == Id &&
               model.Name == Name &&
               model.Surname == Surname &&
               model.Phone == Phone &&
               model.CompanyId == CompanyId &&
               model.DepartmentId == DepartmentId;
    }
}