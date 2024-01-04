using EmployeeWebService.BLL;
using EmployeeWebService.Contracts;
using EmployeeWebService.DAL;
using EmployeeWebService.Models.Exceptions;
using EmployeeWebService.Models.ResponseModels;
using EmployeeWebService.Models.ViewModels;
using EmployeeWebService.Tests.TestCaseSources;
using Moq;
using NUnit.Framework;

namespace EmployeeWebService.Tests;

public class EmployeeBllTests
{
    private EmployeeManager _manager;
    private Mock<IEmployeeRepository> _mockEmp;
    private Mock<IDepartmentRepository> _mockDep;
    private Mock<IPassportRepository> _mockPas;

    [SetUp]
    public void SetUp()
    {
        _mockEmp = new Mock<IEmployeeRepository>();
        _mockDep = new Mock<IDepartmentRepository>();
        _mockPas = new Mock<IPassportRepository>();
        _manager = new EmployeeManager(_mockEmp.Object, _mockDep.Object, _mockPas.Object);
    }

    [TestCaseSource(typeof(EmployeeBllSources))]
    public async Task AddEmployeeAsyncTest(EmployeeRequestModel model, int expected)
    {
        _mockEmp.Setup(o => o.AddEmployeeAsync(model)).Returns(Task.FromResult(expected)).Verifiable();
        _mockPas.Setup(o => o.IsExistAsync(model.PassportId)).Returns(Task.FromResult(true)).Verifiable();
        _mockDep.Setup(o => o.IsExistAsync(model.DepartmentId)).Returns(Task.FromResult(true)).Verifiable();
        _mockEmp.Setup(o => o.IsSamePassportExistAsync(model.PassportId)).Returns(Task.FromResult(false)).Verifiable();
        
        int actual = await _manager.AddEmployeeAsync(model);
        _mockEmp.VerifyAll();
        _mockPas.VerifyAll();
        _mockDep.VerifyAll();
        Assert.AreEqual(expected, actual);
    }

    [TestCaseSource(typeof(EmployeeDeleteBllSources))]
    public async Task DeleteEmployeeAsyncTest(int id, bool expected)
    {
        _mockEmp.Setup(o => o.IsExistAsync(id)).Returns(Task.FromResult(true)).Verifiable();
        _mockEmp.Setup(o => o.DeleteEmployeeAsync(id)).Returns(Task.FromResult(1)).Verifiable();

        bool actual = await _manager.DeleteEmployeeAsync(id);
        _mockEmp.VerifyAll();
        Assert.AreEqual(expected, actual);
    }

    [TestCaseSource(typeof(EmployeeByCompanyBllSources))]
    public async Task GetEmployeesByCompanyIdAsyncTest(int id, IEnumerable<EmployeeResponseModel> expected)
    {
        _mockEmp.Setup(o => o.GetEmployeesByCompanyIdAsync(id)).Returns(Task.FromResult(expected)).Verifiable();

        IEnumerable<EmployeeResponseModel> actual = await _manager.GetEmployeesByCompanyIdAsync(id);
        _mockEmp.VerifyAll();
        
        Assert.AreEqual(expected, actual);
    }

    [TestCaseSource(typeof(EmployeeByDepartmentBllSources))]
    public async Task GetEmployeesByDepartmentIdAsyncTest(int id, IEnumerable<EmployeeResponseModel> expected)
    {
        _mockEmp.Setup(o => o.GetEmployeesByDepartmentIdAsync(id)).Returns(Task.FromResult(expected)).Verifiable();
        _mockDep.Setup(o => o.IsExistAsync(id)).Returns(Task.FromResult(true)).Verifiable();
        
        IEnumerable<EmployeeResponseModel> actual = await _manager.GetEmployeesByDepartmentIdAsync(id);
        _mockEmp.VerifyAll();

        Assert.AreEqual(expected, actual);
    }

    [TestCaseSource(typeof(EmployeeUpdateBllSources))]
    public async Task UpdateEmployeeAsyncTest(EmployeeUpdateModel model, bool expected)
    {
        _mockEmp.Setup(o => o.IsExistAsync(model.Id)).Returns(Task.FromResult(true)).Verifiable();
        _mockDep.Setup(o => o.IsExistAsync((int)model.DepartmentId)).Returns(Task.FromResult(true)).Verifiable();
        _mockEmp.Setup(o => o.UpdateEmployeeAsync(model)).Returns(Task.FromResult(1)).Verifiable();

        bool actual = await _manager.UpdateEmployeeAsync(model);
        _mockEmp.VerifyAll();

        Assert.AreEqual(expected, actual);
    }

    [TestCaseSource(typeof(EmployeeBllSources))]
    public async Task AddEmployeeThrowEntityNotFoundExceptionWhenNoPassportWithSuchId(EmployeeRequestModel model, int expected)
    {
        _mockEmp.Setup(o => o.AddEmployeeAsync(model)).Returns(Task.FromResult(expected));
        _mockPas.Setup(o => o.IsExistAsync(model.PassportId)).Returns(Task.FromResult(false));

        Assert.ThrowsAsync<EntityNotFoundException>(() => _manager.AddEmployeeAsync(model));
    }

    [TestCaseSource(typeof(EmployeeBllSources))]
    public async Task AddEmployeeThrowEntityNotFoundExceptionWhenNoDepartmentWithSuchId(EmployeeRequestModel model, int expected)
    {
        _mockEmp.Setup(o => o.AddEmployeeAsync(model)).Returns(Task.FromResult(expected));
        _mockPas.Setup(o => o.IsExistAsync(model.PassportId)).Returns(Task.FromResult(true));
        _mockDep.Setup(o => o.IsExistAsync(model.DepartmentId)).Returns(Task.FromResult(false));

        Assert.ThrowsAsync<EntityNotFoundException>(() => _manager.AddEmployeeAsync(model));
    }

    [TestCaseSource(typeof(EmployeeBllSources))]
    public async Task AddEmployeeThrowDuplicateAddingAttemptedExceptionWhenSamePersonExist(EmployeeRequestModel model, int expected)
    {
        _mockEmp.Setup(o => o.AddEmployeeAsync(model)).Returns(Task.FromResult(expected));
        _mockPas.Setup(o => o.IsExistAsync(model.PassportId)).Returns(Task.FromResult(true));
        _mockDep.Setup(o => o.IsExistAsync(model.DepartmentId)).Returns(Task.FromResult(true));
        _mockEmp.Setup(o => o.IsSamePassportExistAsync(model.PassportId)).Returns(Task.FromResult(true));

        Assert.ThrowsAsync<DuplicateAddingAttemptedException>(() => _manager.AddEmployeeAsync(model));
    }

    [TestCaseSource(typeof(EmployeeDeleteBllSources))]
    public async Task DeleteEmployeeThrowEntityNotFoundExceptionWhenNoEmployeeWithSuchId(int id, bool expected)
    {
        _mockEmp.Setup(o => o.IsExistAsync(id)).Returns(Task.FromResult(false));
        _mockEmp.Setup(o => o.DeleteEmployeeAsync(id)).Returns(Task.FromResult(1));
        Assert.ThrowsAsync<EntityNotFoundException>(() => _manager.DeleteEmployeeAsync(id));
    }

    [TestCaseSource(typeof(EmployeeDeleteBllSources))]
    public async Task DeleteEmployeeThrowMultipleUpdateExceptionWhenFewRowsWasUpdated(int id, bool expected)
    {
        _mockEmp.Setup(o => o.IsExistAsync(id)).Returns(Task.FromResult(true));
        _mockEmp.Setup(o => o.DeleteEmployeeAsync(id)).Returns(Task.FromResult(2));
        Assert.ThrowsAsync<MultipleUpdateException>(() => _manager.DeleteEmployeeAsync(id));
    }

    [TestCaseSource(typeof(EmployeeByDepartmentBllSources))]
    public async Task GetEmployeesByDepartmentIdThrowEntityNotFoundExceptionWhenNoDepartmentWithSuchId(int id, IEnumerable<EmployeeResponseModel> expected)
    {
        _mockEmp.Setup(o => o.GetEmployeesByDepartmentIdAsync(id)).Returns(Task.FromResult(expected));
        _mockDep.Setup(o => o.IsExistAsync(id)).Returns(Task.FromResult(false));

        Assert.ThrowsAsync<EntityNotFoundException>(() => _manager.GetEmployeesByDepartmentIdAsync(id));
    }

    [TestCaseSource(typeof(EmployeeUpdateNegativeBllSources))]
    public async Task UpdateEmployeeThrowEntityNotFoundExceptionWhenNoEmployeeWithSuchId(EmployeeUpdateModel model)
    {
        _mockEmp.Setup(o => o.IsExistAsync(model.Id)).Returns(Task.FromResult(false));
        _mockDep.Setup(o => o.IsExistAsync((int)model.DepartmentId)).Returns(Task.FromResult(true));
        _mockEmp.Setup(o => o.UpdateEmployeeAsync(model)).Returns(Task.FromResult(1));

        Assert.ThrowsAsync<EntityNotFoundException>(() => _manager.UpdateEmployeeAsync(model));
    }

    [TestCaseSource(typeof(EmployeeUpdateNegativeBllSources))]
    public async Task UpdateEmployeeThrowEntityNotFoundExceptionWhenNoDepartmentWithSuchId(EmployeeUpdateModel model)
    {
        _mockEmp.Setup(o => o.IsExistAsync(model.Id)).Returns(Task.FromResult(true));
        _mockDep.Setup(o => o.IsExistAsync((int)model.DepartmentId)).Returns(Task.FromResult(false));
        _mockEmp.Setup(o => o.UpdateEmployeeAsync(model)).Returns(Task.FromResult(1));

        Assert.ThrowsAsync<EntityNotFoundException>(() => _manager.UpdateEmployeeAsync(model));
    }

    [TestCaseSource(typeof(EmployeeUpdateNegativeBllSources))]
    public async Task UpdateEmployeeThrowMultipleUpdateExceptionWhenFewRowsWasUpdated(EmployeeUpdateModel model)
    {
        _mockEmp.Setup(o => o.IsExistAsync(model.Id)).Returns(Task.FromResult(true));
        _mockDep.Setup(o => o.IsExistAsync((int)model.DepartmentId)).Returns(Task.FromResult(true));
        _mockEmp.Setup(o => o.UpdateEmployeeAsync(model)).Returns(Task.FromResult(2));

        Assert.ThrowsAsync<MultipleUpdateException>(() => _manager.UpdateEmployeeAsync(model));
    }

}