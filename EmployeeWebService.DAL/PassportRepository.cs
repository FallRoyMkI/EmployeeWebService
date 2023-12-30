using System.Collections.Generic;
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

    public int? GetPassportId(PassportRequestModel model)
    {
        var query = @"SELECT Id FROM [dbo].[Passports]
        WHERE Type = @Type AND Number = @Number";

        var parameters = new DynamicParameters();
        parameters.Add("@Type", model.Type);
        parameters.Add("@Number", model.Number);

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        return connection.QuerySingleOrDefault<int?>(query, parameters);
    }

    public int AddPassport(PassportRequestModel model)
    {
        var query = @"INSERT INTO [dbo].[Passports] (Type, Number)
        OUTPUT INSERTED.Id
        VALUES (@Type, @Number)";

        var parameters = new DynamicParameters();
        parameters.Add("@Type", model.Type);
        parameters.Add("@Number", model.Number);

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        return connection.QuerySingle<int>(query, parameters);
    }

    public bool IsExist(int id)
    {
        var query = @"SELECT COUNT(*) FROM [dbo].[Passports]
        WHERE Id = @Id";

        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        return connection.QuerySingle<int>(query, parameters) > 0;
    }

    public void UpdatePassport(PassportUpdateModel model)
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

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        connection.Execute(query.ToString(), parameters);
    }
}