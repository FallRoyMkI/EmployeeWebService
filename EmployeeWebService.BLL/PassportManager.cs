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

    public async Task<int> GetOrAddPassportAsync(PassportRequestModel passport)
    {
        return await _passportRepository.GetPassportIdAsync(passport) ?? await _passportRepository.AddPassportAsync(passport);
    }

    public async Task<bool> UpdatePassportAsync(PassportUpdateModel model)
    {
        if (!await _passportRepository.IsExistAsync(model.Id))
        {
            throw new EntityNotFoundException("There is no passport with this id");
        }
        if (model.Number == null && model.Type == null)
        {
            throw new PointlessUpdateException("Fields to update were empty");
        }
        if (model.Number != null && model.Type != null)
        {
            PassportRequestModel passport = new()
            {
                Number = model.Number,
                Type = model.Type,
            };
            if (await _passportRepository.GetPassportIdAsync(passport) != null)
            {
                throw new DuplicateAddingAttemptedException("Passport with same parameters already exist");
            }
        }
        
        int response = await _passportRepository.UpdatePassportAsync(model);

        if (response == 1)
        {
            return true;
        }
        throw new MultipleUpdateException("Was updated more/less than 1 row");
    }
}