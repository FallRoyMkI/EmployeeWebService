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
                IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Companies')
                    CREATE TABLE [dbo].[Companies] (
                    [Id] [int] IDENTITY(1, 1) PRIMARY KEY NOT NULL,
                    [Name] [nvarchar] (100) NOT NULL)");

        connection.Execute(@"
                IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Departments')    
                    CREATE TABLE [dbo].[Departments] (
                    [Id] [int] IDENTITY(1, 1) PRIMARY KEY NOT NULL,
                    [Name] [nvarchar](100) NOT NULL,
	                [Phone] [nvarchar](20) NOT NULL)");

        connection.Execute(@"
                IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Passports')
                    CREATE TABLE [dbo].[Passports] (
                    [Id] [int] IDENTITY(1, 1) PRIMARY KEY NOT NULL,
                    [Type] [nvarchar](100) NOT NULL,
	                [Number] [nvarchar](30) NOT NULL)");

        connection.Execute(@"
                IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Employees')
                    CREATE TABLE [dbo].[Employees] (
                    [Id] [int] IDENTITY(1, 1) PRIMARY KEY NOT NULL,
                    [Name] [nvarchar](50) NOT NULL,
	                [Surname] [nvarchar](50) NOT NULL,
	                [Phone] [nvarchar](20) NOT NULL,
	                [CompanyId] [int] NOT NULL,
	                [PassportId] [int] NOT NULL,
	                [DepartmentId] [int] NOT NULL,
                    FOREIGN KEY (CompanyId) REFERENCES Companies(Id),
                    FOREIGN KEY (DepartmentId) REFERENCES Departments(Id),
                    FOREIGN KEY (PassportId) REFERENCES Passports(Id))");

        connection.Execute(@"
                IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_SCHEMA = 'dbo' 
                            AND ROUTINE_NAME = 'AddEmployee')
                    EXEC('
                    CREATE PROCEDURE [dbo].[AddEmployee]
                    @Name NVARCHAR(50),
                    @Surname NVARCHAR(50),
	                @Phone NVARCHAR(20),
	                @CompanyId int,
	                @PassportId int,
	                @DepartmentId int,
                    @Id INT OUTPUT
                    AS
                    BEGIN
                    INSERT INTO Employees (Name, Surname, Phone, CompanyId, PassportId, DepartmentId)
                    VALUES (@Name, @Surname, @Phone, @CompanyId, @PassportId, @DepartmentId);
                    SET @Id = SCOPE_IDENTITY();
                    END')");

        connection.Close();
    }
}