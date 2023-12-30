namespace EmployeeWebService.DAL;

public class DatabaseOptions
{
    public const string SectionKey = nameof(DatabaseOptions);

    public string ConnectionString { get; set; }
}