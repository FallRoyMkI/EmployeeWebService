using System.Text;
using Dapper;
using EmployeeWebService.Contracts;
using EmployeeWebService.Models.RequestModels;
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

    public async Task<int?> GetPassportIdAsync(PassportRequestModel model)
    {
        var query = @"SELECT Id FROM [dbo].[Passports]
        WHERE Type = @Type AND Number = @Number";

        var parameters = new DynamicParameters();
        parameters.Add("@Type", model.Type);
        parameters.Add("@Number", model.Number);

        await using var connection = new SqlConnection(_connectionString);
        connection.Open();

        return await connection.QuerySingleOrDefaultAsync<int?>(query, parameters);
    }

    public async Task<int> AddPassportAsync(PassportRequestModel model)
    {
        var query = @"INSERT INTO [dbo].[Passports] (Type, Number)
        OUTPUT INSERTED.Id
        VALUES (@Type, @Number)";

        var parameters = new DynamicParameters();
        parameters.Add("@Type", model.Type);
        parameters.Add("@Number", model.Number);

        await using var connection = new SqlConnection(_connectionString);
        connection.Open();

        return await connection.QuerySingleAsync<int>(query, parameters);
    }

    public async Task<bool> IsExistAsync(int id)
    {
        var query = @"SELECT COUNT(*) FROM [dbo].[Passports]
        WHERE Id = @Id";

        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        await using var connection = new SqlConnection(_connectionString);
        connection.Open();

        return await connection.QuerySingleAsync<int>(query, parameters) > 0;
    }

    public async Task<int> UpdatePassportAsync(PassportUpdateModel model)
    {
        StringBuilder query = new(@"UPDATE [dbo].[Passports] SET ");
        var parameters = new DynamicParameters();
        bool isNeedCome = false;

        if (model.Type != null)
        {
            query.Append("Type = @Type ");
            parameters.Add("@Type", model.Type);
            isNeedCome = true;
        }

        if (model.Number != null)
        {
            if (isNeedCome)
            {
                query.Append(", ");
            }
            query.Append("Number = @Number ");
            parameters.Add("@Number", model.Number);
        }

        query.Append("WHERE Id = @Id");
        parameters.Add("Id", model.Id);

        await using var connection = new SqlConnection(_connectionString);
        connection.Open();

        return await connection.ExecuteAsync(query.ToString(), parameters);
    }
}