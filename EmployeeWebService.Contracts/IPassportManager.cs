using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.Contracts;

public interface IPassportManager
{
    public int GetOrAddPassport(PassportRequestModel model);
}