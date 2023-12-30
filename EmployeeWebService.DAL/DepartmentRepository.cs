using Dapper;
using EmployeeWebService.Contracts;
using EmployeeWebService.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace EmployeeWebService.DAL;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly string _connectionString;

    public DepartmentRepository(IOptions<DatabaseOptions> options)
    {
        _connectionString = options.Value.ConnectionString;
    }

    public bool IsExist(Department model)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var query = @"
        SELECT COUNT(*) FROM [dbo].[Departments] 
        WHERE NAME = @Name and Phone = @Phone";

        var parameters = new DynamicParameters();
        parameters.Add("@Name", model.Name);
        parameters.Add("@Phone", model.Phone);

        return connection.ExecuteScalar<int>(query, parameters) > 0;
    }

    public void AddDepartment(Department model)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var query = @"
        INSERT INTO [dbo].[Departments] (Name, Phone)
        VALUES (@Name, @Phone)";

        var parameters = new DynamicParameters();
        parameters.Add("@Name", model.Name);
        parameters.Add("@Phone", model.Phone);

        connection.Execute(query, parameters);
        connection.Close();
    }

    public int GetDepartmentId(Department model)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var query = @"
        SELECT Id FROM [dbo].[Departments] 
        WHERE NAME = @Name and Phone = @Phone";

        var parameters = new DynamicParameters();
        parameters.Add("@Name", model.Name);
        parameters.Add("@Phone", model.Phone);

        return connection.QuerySingle<int>(query, parameters);
    }
}