using System.Data;
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

        int id = parameters.Get<int>("@Id");

        connection.Close();

        return id;
    }
}

