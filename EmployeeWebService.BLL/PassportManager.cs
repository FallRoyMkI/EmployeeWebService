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

    public void UpdatePassport(PassportUpdateModel model)
    {
        if (_passportRepository.IsExist(model.Id) && (model.Number != null || model.Type != null))
        {
            _passportRepository.UpdatePassport(model);
        }
    }
}