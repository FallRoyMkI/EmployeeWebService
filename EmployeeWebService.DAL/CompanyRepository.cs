using System.Data;
using Dapper;
using EmployeeWebService.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace EmployeeWebService.DAL;

public class CompanyRepository : ICompanyRepository
{
    private readonly string _connectionString;

    public CompanyRepository(IOptions<DatabaseOptions> options)
    {
        _connectionString = options.Value.ConnectionString;
    }

    public bool IsExist(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var query = @"
        SELECT COUNT(*) FROM [dbo].[Companies] 
        WHERE Id = @Id";

        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        return connection.ExecuteScalar<int>(query, parameters) > 0;
    }

    public int AddCompany()
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var query = @"
        INSERT INTO [dbo].[Companies] (Name)
        OUTPUT INSERTED.Id
        VALUES (@Name)";

        var parameters = new DynamicParameters();
        parameters.Add("@Name", "New Company");
        parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

        return connection.QuerySingle<int>(query, parameters);
    }
}