using System.Collections;
using EmployeeWebService.Models.ViewModels;

namespace EmployeeWebService.Tests.TestCaseSources;

public class DepartmentBllSources : IEnumerable
{
    IEnumerator IEnumerable.GetEnumerator()
    {
        DepartmentRequestModel model = new()
        {
            Name = "testNa",
            Phone = "testPh"
        };
        int result = 0;
        
        yield return new Object[] { model, result};

        model = new()
        {
            Name = "testNa1",
            Phone = "testPh1"
        };
        result = 1;

        yield return new Object[] { model, result };
    }
}