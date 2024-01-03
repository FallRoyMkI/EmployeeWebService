using EmployeeWebService.Models.RequestModels;
using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.Contracts;

public interface IPassportRepository
{
    public Task<int?> GetPassportIdAsync(PassportRequestModel model);
    public Task<int> AddPassportAsync(PassportRequestModel model);
    public Task<bool> IsExistAsync(int id);
    public Task<int> UpdatePassportAsync(PassportUpdateModel model);
}