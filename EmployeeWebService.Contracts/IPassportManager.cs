using EmployeeWebService.Models.RequestModels;
using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.Contracts;

public interface IPassportManager
{
    public int GetOrAddPassport(PassportRequestModel model);
    public int UpdatePassport(PassportUpdateModel model);
}