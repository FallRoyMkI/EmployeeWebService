using System.Collections;
using EmployeeWebService.Models.RequestModels;
using EmployeeWebService.Models.ResponseModels;
using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.Tests.TestCaseSources;

public class EmployeeBllSources : IEnumerable
{
    IEnumerator IEnumerable.GetEnumerator()
    {
        EmployeeRequestModel model = new()
        {
            Name = "testName",
            Surname = "testSurname",
            Phone = "testPhone",
            PassportId = 1,
            DepartmentId = 1,
            CompanyId = 1
        };
        int result = 2;
        yield return new Object[] { model, result };
    }
}

public class EmployeeDeleteBllSources : IEnumerable
{
    IEnumerator IEnumerable.GetEnumerator()
    {
        int id = 1;
        bool expected = true;
        yield return new Object[] { id, expected };
    }
}


public class EmployeeByCompanyBllSources : IEnumerable
{
    IEnumerator IEnumerable.GetEnumerator()
    {
        int id = 1;
        List<EmployeeResponseModel> result = new List<EmployeeResponseModel>();
        yield return new Object[] { id, result };
        id = 2;
        PassportRequestModel passport = new PassportRequestModel()
        {
            Number = "testPass",
            Type = "testType"
        };
        DepartmentRequestModel department = new DepartmentRequestModel()
        {
            Name = "testDep",
            Phone = "testPh"
        };
        EmployeeResponseModel model = new()
        {
            Name = "testName",
            Surname = "testSurname",
            Phone = "testPhone",
            CompanyId = id,
            Passport = passport,
            Department = department
        };
        result.Add(model);

        yield return new Object[] { id, result };
    }
}


public class EmployeeByDepartmentBllSources : IEnumerable
{
    IEnumerator IEnumerable.GetEnumerator()
    {
        int id = 1;
        List<EmployeeResponseModel> result = new List<EmployeeResponseModel>();
        yield return new Object[] { id, result };
        id = 2;
        PassportRequestModel passport = new PassportRequestModel()
        {
            Number = "testPass",
            Type = "testType"
        };
        DepartmentRequestModel department = new DepartmentRequestModel()
        {
            Name = "testDep",
            Phone = "testPh"
        };
        EmployeeResponseModel model = new()
        {
            Name = "testName",
            Surname = "testSurname",
            Phone = "testPhone",
            CompanyId = 1,
            Passport = passport,
            Department = department
        };
        result.Add(model);

        yield return new Object[] { id, result };
    }
}


public class EmployeeUpdateBllSources : IEnumerable
{
    IEnumerator IEnumerable.GetEnumerator()
    {
        bool result = true;
        EmployeeUpdateModel model = new()
        {
            Id = 1,
            Name = "testName",
            Surname = "testSurname",
            Phone = "testPhone",
            DepartmentId = 1,
            CompanyId = 1
        };

        yield return new Object[] { model, result };
    }
}


public class EmployeeUpdateNegativeBllSources : IEnumerable
{
    IEnumerator IEnumerable.GetEnumerator()
    {
        EmployeeUpdateModel model = new()
        {
            Id = 1,
            Name = "testName",
            Surname = "testSurname",
            Phone = "testPhone",
            DepartmentId = 1,
            CompanyId = 1
        };

        yield return new Object[] { model };
    }
}
