using EmployeeWebService.BLL;
using EmployeeWebService.Contracts;
using EmployeeWebService.Models.Exceptions;
using EmployeeWebService.Models.RequestModels;
using EmployeeWebService.Models.ViewModels;
using EmployeeWebService.Tests.TestCaseSources;
using Moq;
using NUnit.Framework;

namespace EmployeeWebService.Tests;

public class PassportBllTests
{
    private PassportManager _manager;
    private Mock<IPassportRepository> _mock;

    [SetUp]
    public void SetUp()
    {
        _mock = new Mock<IPassportRepository>();
        _manager = new PassportManager(_mock.Object);
    }

    [TestCaseSource(typeof(PassportBllSources))]
    public async Task GetPassportIdAsyncTest(PassportRequestModel model, int expected)
    {
        _mock.Setup(o => o.GetPassportIdAsync(model)).Returns(Task.FromResult((int?)expected)).Verifiable();

        int actual = await _manager.GetOrAddPassportAsync(model);
        _mock.VerifyAll();
        Assert.AreEqual(expected, actual);
    }

    [TestCaseSource(typeof(PassportBllSources))]
    public async Task AddDepartmentIdAsync(PassportRequestModel model, int expected)
    {
        _mock.Setup(o => o.AddPassportAsync(model)).Returns(Task.FromResult(expected)).Verifiable();

        int actual = await _manager.GetOrAddPassportAsync(model);
        _mock.VerifyAll();
        Assert.AreEqual(expected, actual);
    }

    [TestCaseSource(typeof(PassportUpdateBllSources))]
    public async Task UpdatePassportByIdAsync(PassportUpdateModel model, int result, bool expected)
    {
        _mock.Setup(o => o.IsExistAsync(model.Id)).Returns(Task.FromResult(expected)).Verifiable();
        _mock.Setup(o => o.UpdatePassportAsync(model)).Returns(Task.FromResult(result)).Verifiable();
        bool actual = await _manager.UpdatePassportAsync(model);
        _mock.VerifyAll();
        Assert.AreEqual(expected, actual);
    }

    [TestCaseSource(typeof(PassportUpdateBllIdNotExistSources))]
    public async Task UpdatePassportByIdThrowEntityNotFoundExceptionWhenNoPassportWithSuchId(PassportUpdateModel model, bool result)
    {
        _mock.Setup(o => o.IsExistAsync(model.Id)).Returns(Task.FromResult(result));
        Assert.ThrowsAsync<EntityNotFoundException>(() => _manager.UpdatePassportAsync(model));
    }

    [TestCaseSource(typeof(PassportUpdateBllFieldsAreEmptySources))]
    public async Task UpdatePassportByIdThrowPointlessUpdateExceptionWhenUpdateFieldsAreEmpty(PassportUpdateModel model, bool result)
    {
        _mock.Setup(o => o.IsExistAsync(model.Id)).Returns(Task.FromResult(result));
        Assert.ThrowsAsync<PointlessUpdateException>(() => _manager.UpdatePassportAsync(model));
    }

    [TestCaseSource(typeof(PassportUpdateBllSamePassportExistSources))]
    public async Task UpdatePassportByIdThrowDuplicateAddingAttemptedExceptionWhenSamePassportExist
        (PassportUpdateModel model, bool result, PassportRequestModel entity, int? response)
    {
        _mock.Setup(o => o.IsExistAsync(model.Id)).Returns(Task.FromResult(result));
        _mock.Setup(o => o.GetPassportIdAsync(entity)).Returns(Task.FromResult(response));

        Assert.ThrowsAsync<DuplicateAddingAttemptedException>(() => _manager.UpdatePassportAsync(model));
    }

    [TestCaseSource(typeof(PassportUpdateBllFewRowsAffectedSources))]
    public async Task UpdatePassportByIdThrowMultipleUpdateExceptionWhenFewRowsWasAffected
        (PassportUpdateModel model, bool result, int? response)
    {
        _mock.Setup(o => o.IsExistAsync(model.Id)).Returns(Task.FromResult(result));

        Assert.ThrowsAsync<MultipleUpdateException>(() => _manager.UpdatePassportAsync(model));
    }
}