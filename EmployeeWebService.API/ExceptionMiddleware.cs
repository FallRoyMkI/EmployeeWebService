using System.Text.Json;
using EmployeeWebService.Models.Exceptions;
using EmployeeWebService.Models.ResponseModels;
using Microsoft.Data.SqlClient;

namespace EmployeeWebService.API;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex)
        {
            await HandlerExceptionAsync(context, ex);
        }
    }

    private static Task HandlerExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        string message = ex.Message;
        int code = ex switch
        {
            EntityNotFoundException => 404,
            SqlException => 500,
            _ => 400
        };
        ExceptionResponseModel exR = new(message, code);
        string result = JsonSerializer.Serialize(exR);
        context.Response.StatusCode = exR.Code;
        return context.Response.WriteAsync(result);
    }

}