using System.Collections;
using EmployeeWebService.Models.RequestModels;
using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.Tests.TestCaseSources;

public class PassportBllSources : IEnumerable
{
    IEnumerator IEnumerable.GetEnumerator()
    {
        PassportRequestModel model = new()
        {
            Number = "testNu", 
            Type = "testTy"
        };
        int result = 0;

        yield return new Object[] { model, result };

        model = new()
        {
            Number = "testNu1",
            Type = "testTy1"
        };
        result = 1;

        yield return new Object[] { model, result };
    }
}

public class PassportUpdateBllSources : IEnumerable
{
    IEnumerator IEnumerable.GetEnumerator()
    {
        PassportUpdateModel model = new()
        {
            Id = 1,
            Number = "testNu",
            Type = "testTy"
        };
        int result = 1;
        bool response = true;

        yield return new Object[] { model, result, response };

        model = new()
        {
            Id = 2,
            Number = "testNu1",
            Type = "testTy1"
        };
        result = 1;
        response = true;

        yield return new Object[] { model, result, response };
    }
}

public class PassportUpdateBllIdNotExistSources : IEnumerable
{
    IEnumerator IEnumerable.GetEnumerator()
    {
        PassportUpdateModel model = new()
        {
            Id = 1,
            Number = "testNu",
            Type = "testTy"
        };
        bool response = false;

        yield return new Object[] { model, response };

        model = new()
        {
            Id = 2,
            Number = "testNu1",
            Type = "testTy1"
        };
        response = false;

        yield return new Object[] { model, response };
    }
}

public class PassportUpdateBllFieldsAreEmptySources : IEnumerable
{
    IEnumerator IEnumerable.GetEnumerator()
    {
        PassportUpdateModel model = new()
        {
            Id = 1,
            Number = null,
            Type = null
        };
        bool response = true;

        yield return new Object[] { model, response };

        model = new()
        {
            Id = 2,
            Number = String.Empty,
            Type = String.Empty
        };
        response = true;

        yield return new Object[] { model, response };
    }
}

public class PassportUpdateBllSamePassportExistSources : IEnumerable
{
    IEnumerator IEnumerable.GetEnumerator()
    {
        PassportUpdateModel model = new()
        {
            Id = 1,
            Number = "testNu",
            Type = "testTy"
        };
        bool response = true;
        PassportRequestModel request = new()
        {
            Number = "testNu",
            Type = "testTy"
        };
        int result = 1;
        yield return new Object[] { model, response, request, result };

        model = new()
        {
            Id = 2,
            Number = "testNu1",
            Type = "testTy1"
        };
        response = true;
        request = new()
        {
            Number = "testNu1",
            Type = "testTy1"
        };
        result = 3;
        yield return new Object[] { model, response, request, result };
    }
}

public class PassportUpdateBllFewRowsAffectedSources : IEnumerable
{
    IEnumerator IEnumerable.GetEnumerator()
    {
        PassportUpdateModel model = new()
        {
            Id = 1,
            Number = "testNu",
            Type = "testTy"
        };
        bool response = true;
        int result = 2;
        yield return new Object[] { model, response, result };

        model = new()
        {
            Id = 2,
            Number = "testNu1",
            Type = "testTy1"
        };
        response = true;
        result = 3;
        yield return new Object[] { model, response, result };
    }
}