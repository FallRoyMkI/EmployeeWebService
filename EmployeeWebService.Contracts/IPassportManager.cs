using EmployeeWebService.Models.RequestModels;
using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.Contracts;

public interface IPassportManager
{
    public Task<int> GetOrAddPassportAsync(PassportRequestModel model);
    public Task<bool> UpdatePassportAsync(PassportUpdateModel model);
}