using EmployeeWebService.Models.RequestModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeWebService.Models.ViewModels;

public class DepartmentRequestModel
{
    public string Name { get; set; }
    public string Phone { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is DepartmentRequestModel model &&
               model.Name == Name &&
               model.Phone == Phone;
    }
}