using Dapper;
using EmployeeWebService.Contracts;
using EmployeeWebService.Models.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace EmployeeWebService.DAL;

public class PassportRepository : IPassportRepository
{
    private readonly string _connectionString;

    public PassportRepository(IOptions<DatabaseOptions> options)
    {
        _connectionString = options.Value.ConnectionString;
    }

    public int? GetPassportId(PassportRequestModel model)
    {
        var query = @"SELECT Id FROM [dbo].[Passports]
        WHERE Type = @Type AND Number = @Number";

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("@Type", model.Type);
        parameters.Add("@Number", model.Number);

        return connection.QuerySingleOrDefault<int?>(query, parameters);
    }

    public int AddPassport(PassportRequestModel model)
    {
        var query = @"INSERT INTO [dbo].[Passports] (Type, Number)
        OUTPUT INSERTED.Id
        VALUES (@Type, @Number)";

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("@Type", model.Type);
        parameters.Add("@Number", model.Number);

        return connection.QuerySingle<int>(query, parameters);
    }

    public bool IsExist(int id)
    {
        var query = @"SELECT COUNT(*) FROM [dbo].[Passports]
        WHERE Id = @Id";

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        return connection.QuerySingle<int>(query, parameters) > 0;
    }
}