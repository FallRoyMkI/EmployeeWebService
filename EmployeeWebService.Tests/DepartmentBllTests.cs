using EmployeeWebService.BLL;
using EmployeeWebService.Contracts;
using EmployeeWebService.Models.ViewModels;
using EmployeeWebService.Tests.TestCaseSources;
using Moq;
using NUnit.Framework;

namespace EmployeeWebService.Tests;

public class DepartmentBllTests
{
    private DepartmentManager _manager;
    private Mock<IDepartmentRepository> _mock;

    [SetUp]
    public void SetUp()
    {
        _mock = new Mock<IDepartmentRepository>();
        _manager = new DepartmentManager(_mock.Object);
    }

    [TestCaseSource(typeof(DepartmentBllSources))]
    public async Task GetDepartmentIdAsyncTest(DepartmentRequestModel model, int expected)
    {
        _mock.Setup(o => o.GetDepartmentIdAsync(model)).Returns(Task.FromResult((int?)expected)).Verifiable();

        int actual = await _manager.GetOrAddDepartmentAsync(model);
        _mock.VerifyAll();
        Assert.AreEqual(expected, actual);
    }

    [TestCaseSource(typeof(DepartmentBllSources))]
    public async Task AddDepartmentIdAsyncTest(DepartmentRequestModel model, int expected)
    {
        _mock.Setup(o => o.AddDepartmentAsync(model)).Returns(Task.FromResult(expected)).Verifiable();

        int actual = await _manager.GetOrAddDepartmentAsync(model);
        _mock.VerifyAll();
        Assert.AreEqual(expected, actual);
    }
}