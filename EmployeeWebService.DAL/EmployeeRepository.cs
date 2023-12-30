using System.Data;
using System.Reflection;
using Dapper;
using EmployeeWebService.Contracts;
using EmployeeWebService.Models.Entities;
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

    public int AddEmployee(Employee model)
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

    public void DeleteEmployee(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        connection.Execute("DeleteEmployee", parameters, commandType: CommandType.StoredProcedure);
    }

    public bool IsExist(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var query = @"
        SELECT COUNT(*) FROM [dbo].[Employees] 
        WHERE Id = @Id AND IsDeleted = 0";

        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        return connection.ExecuteScalar<int>(query, parameters) > 0;
    }
}

