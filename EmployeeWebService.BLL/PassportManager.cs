using EmployeeWebService.Contracts;
using EmployeeWebService.DAL;
using EmployeeWebService.Models.RequestModels;
using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.BLL;

public class PassportManager : IPassportManager
{
    private readonly IPassportRepository _passportRepository;
    public PassportManager(IPassportRepository passportRepository)
    {
        _passportRepository = passportRepository;
    }

    public int GetOrAddPassport(PassportRequestModel passport)
    {
        return _passportRepository.GetPassportId(passport) ?? _passportRepository.AddPassport(passport);
    }

    public int UpdatePassport(PassportUpdateModel model)
    {
        if (_passportRepository.IsExist(model.Id) && (model.Number != null || model.Type != null))
        {
            return _passportRepository.UpdatePassport(model);
        }

        throw new Exception("Pointless attempt to update");
    }
}