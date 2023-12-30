﻿using EmployeeWebService.Contracts;
using EmployeeWebService.DAL;
using EmployeeWebService.Models.Entities;
using EmployeeWebService.Models.ViewModels;
using Microsoft.Extensions.Options;

namespace EmployeeWebService.BLL
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IPassportRepository _passportRepository;
        public EmployeeManager(IEmployeeRepository employeeRepository, 
            IDepartmentRepository departmentRepository, IPassportRepository passportRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _passportRepository = passportRepository;
        }

        public int AddEmployee(EmployeeViewModel model)
        {
            Department department = new()
            {
                Name = model.Name,
                Phone = model.Phone
            };
            if (!_departmentRepository.IsExist(department))
            {
                _departmentRepository.AddDepartment(department);
            }
            int departmentId = _departmentRepository.GetDepartmentId(department);

            Passport passport = new()
            {
                Type = model.Passport.Type,
                Number = model.Passport.Number
            };
            int passportId = _passportRepository.AddPassport(passport);

            Employee entity = new Employee()
            {
                Name = model.Name,
                Surname = model.Surname,
                Phone = model.Phone,
                CompanyId = model.CompanyId,
                DepartmentId = departmentId,
                PassportId = passportId
            };

            return _employeeRepository.AddEmployee(entity);
        }
    }
}
