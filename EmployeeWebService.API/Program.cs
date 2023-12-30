using EmployeeWebService.API;
using EmployeeWebService.BLL;
using EmployeeWebService.Contracts;
using EmployeeWebService.DAL;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var d = new DatabaseOptions();
var databaseSection = builder.Configuration.GetSection(DatabaseOptions.SectionKey);
databaseSection.Bind(d);
builder.Services.AddSingleton(Options.Create(d));
builder.Services.AddSingleton<DatabaseInitializer>();

builder.Services.AddScoped<IPassportManager, PassportManager>();
builder.Services.AddScoped<IPassportRepository, PassportRepository>();

builder.Services.AddScoped<IDepartmentManager, DepartmentManager>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddScoped<IEmployeeManager, EmployeeManager>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();


builder.Services.BuildServiceProvider().GetService<DatabaseInitializer>().Initialize();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
