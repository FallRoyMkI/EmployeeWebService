using EmployeeWebService.Contracts;
using EmployeeWebService.Models.Exceptions;
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
        if (!_passportRepository.IsExist(model.Id))
        {
            throw new EntityNotFoundException("There is no passport with this id");
        }
        if (model.Number == null || model.Type == null)
        {
            throw new PointlessUpdateException("Fields to update were empty");

        }

        return _passportRepository.UpdatePassport(model);
    }
}