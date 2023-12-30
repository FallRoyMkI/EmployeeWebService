using Dapper;
using EmployeeWebService.Contracts;
using EmployeeWebService.Models.Entities;
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

    public int AddPassport(Passport model)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var query = @"
        INSERT INTO [dbo].[Passports] (Type, Number)
        OUTPUT INSERTED.Id
        VALUES (@Type, @Number)";

        return connection.QuerySingle<int>(query, model);
    }
}