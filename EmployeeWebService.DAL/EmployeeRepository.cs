using System.Data;
using System.Text;
using Dapper;
using EmployeeWebService.Contracts;
using EmployeeWebService.Models;
using EmployeeWebService.Models.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace EmployeeWebService.DAL;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly string _connectionString;

    public EmployeeRepository(IOptions<DatabaseOptions> options)
    {
        _connectionString = options.Value.ConnectionString;
    }

    public int AddEmployee(EmployeeRequestModel model)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("@Name", model.Name);
        parameters.Add("@Surname", model.Surname);
        parameters.Add("@Phone", model.Phone);
        parameters.Add("@CompanyId", model.CompanyId);
        parameters.Add("@PassportId", model.PassportId);
        parameters.Add("@DepartmentId", model.DepartmentId);
        parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

        connection.Execute("AddEmployee", parameters, commandType: CommandType.StoredProcedure);

        return parameters.Get<int>("@Id");
    }

    public bool IsExist(int id)
    {
        var query = @"SELECT COUNT(*) FROM [dbo].[Employees] 
        WHERE Id = @Id AND IsDeleted = 0";

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        return connection.ExecuteScalar<int>(query, parameters) > 0;
    }

    public bool IsSamePassportExist(int id)
    {
        var query = @"SELECT COUNT(*) FROM [dbo].[Employees] 
        WHERE PassportId = @Id AND IsDeleted = 0";

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        return connection.ExecuteScalar<int>(query, parameters) > 0;
    }

    public void DeleteEmployee(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        connection.Execute("DeleteEmployee", parameters, commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<EmployeeResponseModel> GetEmployeesByCompanyId(int id)
    {
        string sqlQuery = @"SELECT E.Id, E.Name, E.Surname, E.Phone, E.CompanyId, P.*, D.*
        FROM Employees AS E
        LEFT JOIN Passports AS P ON E.PassportId = P.Id
        JOIN Departments AS D ON E.DepartmentId = D.Id
        WHERE E.CompanyId = @Id AND E.IsDeleted = 0;";

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        return connection.Query<EmployeeResponseModel, PassportRequestModel, DepartmentRequestModel, EmployeeResponseModel>
        (sqlQuery,
            (employee, passport, department) =>
            {
                employee.Passport = passport;
                employee.Department = department;
                return employee;
            },
            parameters,
            splitOn: "Id").ToList();
    }

    public IEnumerable<EmployeeResponseModel> GetEmployeesByDepartmentId(int id)
    {
        string sqlQuery = @"SELECT E.Id, E.Name, E.Surname, E.Phone, E.CompanyId, P.*, D.*
        FROM Employees AS E
        LEFT JOIN Passports AS P ON E.PassportId = P.Id
        JOIN Departments AS D ON E.DepartmentId = D.Id
        WHERE E.DepartmentId = @Id AND E.IsDeleted = 0;";

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        return connection.Query<EmployeeResponseModel, PassportRequestModel, DepartmentRequestModel, EmployeeResponseModel>
        (sqlQuery,
            (employee, passport, department) =>
            {
                employee.Passport = passport;
                employee.Department = department;
                return employee;
            },
            parameters,
            splitOn: "Id").ToList();
    }

    public void UpdateEmployee(EmployeeUpdateModel model)
    {
        StringBuilder query = new(@"UPDATE [dbo].[Employees] SET ");
        var parameters = new DynamicParameters();
        bool isNeedCome = false;

        if (model.Name != null)
        {
            query.Append("Name = @Name ");
            parameters.Add("@Name", model.Name);
            isNeedCome = true;
        }

        if (model.Surname != null)
        {
            if (isNeedCome)
            {
                query.Append(", ");
            }
            query.Append("Surname = @Surname ");
            parameters.Add("@Surname", model.Surname);
            isNeedCome = true;
        }

        if (model.Phone != null)
        {
            if (isNeedCome)
            {
                query.Append(", ");
            }
            query.Append("Phone = @Phone ");
            parameters.Add("@Phone", model.Phone);
            isNeedCome = true;
        }

        if (model.CompanyId != null)
        {
            if (isNeedCome)
            {
                query.Append(", ");
            }
            query.Append("CompanyId = @CompanyId ");
            parameters.Add("@CompanyId", model.CompanyId);
            isNeedCome = true;
        }

        if (model.DepartmentId != null)
        {
            if (isNeedCome)
            {
                query.Append(", ");
            }
            query.Append("DepartmentId = @DepartmentId ");
            parameters.Add("@DepartmentId", model.DepartmentId);
        }

        query.Append("WHERE Id = @Id");
        parameters.Add("Id", model.Id);

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        connection.Execute(query.ToString(), parameters);
    }
}
