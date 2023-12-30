using EmployeeWebService.API;
using EmployeeWebService.Contracts;
using EmployeeWebService.DAL;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var databaseSection = builder.Configuration.GetSection(DatabaseOptions.SectionKey);
builder.Services.AddSingleton(Options.Create(databaseSection));
builder.Services.AddSingleton<DatabaseInitializer>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IPassportRepository, PasportRepository>();

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
