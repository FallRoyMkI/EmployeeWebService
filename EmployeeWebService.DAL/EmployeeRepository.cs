using EmployeeWebService.Contracts;
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

    public void AddEmployee()
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();


        connection.Close();
    }
}

