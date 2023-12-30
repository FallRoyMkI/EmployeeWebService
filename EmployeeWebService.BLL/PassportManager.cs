using EmployeeWebService.Contracts;
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
}