using Dapper;
using EmployeeWebService.Contracts;
using EmployeeWebService.Models.ViewModels;
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


    public int? GetDepartmentId(DepartmentRequestModel model)
    {
        var query = @"SELECT Id FROM [dbo].[Departments]
        WHERE Name = @Name AND Phone = @Phone";

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("@Name", model.Name);
        parameters.Add("@Phone", model.Phone);

        return connection.QuerySingleOrDefault<int?>(query, parameters);
    }

    public int AddDepartment(DepartmentRequestModel model)
    {
        var query = @"INSERT INTO [dbo].[Departments] (Name, Phone)
        OUTPUT INSERTED.Id
        VALUES (@Name, @Phone)";

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("@Name", model.Name);
        parameters.Add("@Phone", model.Phone);

        return connection.QuerySingle<int>(query, parameters);
    }

    public bool IsExist(int id)
    {
        var query = @"SELECT COUNT(*) FROM [dbo].[Departments]
        WHERE Id = @Id";

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        return connection.QuerySingle<int>(query, parameters) > 0;
    }
}