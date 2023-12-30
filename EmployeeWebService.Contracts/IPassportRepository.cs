using EmployeeWebService.Models.Entities;

namespace EmployeeWebService.Contracts;

public interface IPassportRepository
{
    public int AddPassport(Passport model);
}