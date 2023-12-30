namespace EmployeeWebService.Contracts;

public interface ICompanyRepository
{
    public bool IsExist(int id);
    public int AddCompany();
}