﻿using EmployeeWebService.Models.Entities;

namespace EmployeeWebService.Contracts;

public interface IEmployeeRepository
{
    public int AddEmployee(Employee model);
}