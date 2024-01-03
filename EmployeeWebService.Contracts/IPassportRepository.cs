using EmployeeWebService.Models.RequestModels;
using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.Contracts;

public interface IPassportRepository
{
    public int? GetPassportId(PassportRequestModel model);
    public int AddPassport(PassportRequestModel model);
    public bool IsExist(int id);
    public int UpdatePassport(PassportUpdateModel model);
}