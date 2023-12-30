using Dapper;
using EmployeeWebService.DAL;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace EmployeeWebService.API;

public class DatabaseInitializer
{
    private readonly string _connectionString;

    public DatabaseInitializer(IOptions<DatabaseOptions> options)
    {
        _connectionString = options.Value.ConnectionString;
    }

    public void Initialize()
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        connection.Execute(@"
                CREATE TABLE IF NOT EXISTS [dbo].[Companies] (
                    [Id] [int] IDENTITY(1, 1) PRIMARY KEY NOT NULL,
                    [Name] [nvarchar] (100) NOT NULL
                )");

        connection.Execute(@"
                CREATE TABLE IF NOT EXISTS [dbo].[Departments] (
                    [Id] [int] IDENTITY(1, 1) PRIMARY KEY NOT NULL,
                    [Name] [nvarchar](100) NOT NULL,
	                [Phone] [nvarchar](20) NOT NULL,
                )");

        connection.Execute(@"
                CREATE TABLE IF NOT EXISTS [dbo].[Passports] (
                    [Id] [int] IDENTITY(1, 1) PRIMARY KEY NOT NULL,
                    [Type] [nvarchar](100) NOT NULL,
	                [Number] [nvarchar](30) NOT NULL,
                )");

        connection.Execute(@"
                CREATE TABLE IF NOT EXISTS [dbo].[Employees] (
                    [Id] [int] IDENTITY(1, 1) PRIMARY KEY NOT NULL,
                    [Name] [nvarchar](50) NOT NULL,
	                [Surname] [nvarchar](50) NOT NULL,
	                [Phone] [nvarchar](20) NOT NULL,
	                [CompanyId] [int] NOT NULL,
	                [PassportId] [int] NOT NULL,
	                [DepartmentId] [int] NOT NULL,
                    FOREIGN KEY (CompanyId) REFERENCES Companies(Id),
                    FOREIGN KEY (DepartmentId) REFERENCES Departments(Id),
                    FOREIGN KEY (PassportId) REFERENCES Passports(Id)
                )");

        connection.Close();
    }
}