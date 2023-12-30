using EmployeeWebService.API;
using EmployeeWebService.BLL;
using EmployeeWebService.Contracts;
using EmployeeWebService.DAL;
using EmployeeWebService.Models.Entities;
using Microsoft.Extensions.Options;
using static System.Collections.Specialized.BitVector32;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var d = new DatabaseOptions();
var databaseSection = builder.Configuration.GetSection(DatabaseOptions.SectionKey);
databaseSection.Bind(d);
builder.Services.AddSingleton(Options.Create(d));
builder.Services.AddSingleton<DatabaseInitializer>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IPassportRepository, PassportRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IEmployeeManager, EmployeeManager>();

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
