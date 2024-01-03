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

    public async Task<int?> GetDepartmentIdAsync(DepartmentRequestModel model)
    {
        var query = @"SELECT Id FROM [dbo].[Departments]
        WHERE Name = @Name AND Phone = @Phone";

        var parameters = new DynamicParameters();
        parameters.Add("@Name", model.Name);
        parameters.Add("@Phone", model.Phone);

        await using var connection = new SqlConnection(_connectionString);
        connection.Open();

        return await connection.QuerySingleOrDefaultAsync<int?>(query, parameters);
    }

    public async Task<int> AddDepartmentAsync(DepartmentRequestModel model)
    {
        var query = @"INSERT INTO [dbo].[Departments] (Name, Phone)
        OUTPUT INSERTED.Id
        VALUES (@Name, @Phone)";

        var parameters = new DynamicParameters();
        parameters.Add("@Name", model.Name);
        parameters.Add("@Phone", model.Phone);

        await using var connection = new SqlConnection(_connectionString);
        connection.Open();

        return await connection.QuerySingleAsync<int>(query, parameters);
    }

    public async Task<bool> IsExistAsync(int id)
    {
        var query = @"SELECT COUNT(*) FROM [dbo].[Departments]
        WHERE Id = @Id";

        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        await using var connection = new SqlConnection(_connectionString);
        connection.Open();

        return await connection.QuerySingleAsync<int>(query, parameters) > 0;
    }
}