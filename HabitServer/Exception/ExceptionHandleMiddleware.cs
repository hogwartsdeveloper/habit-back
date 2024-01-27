using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace HabitServer.Exception;

public class ExceptionHandleMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandleMiddleware> _logger;

    public ExceptionHandleMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandleMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (HttpException e)
        {
            _logger.LogError(e, "Exception occurred: {Message}", e.Message);
            await Handle(context, e.StatusCode, e.Message);
        }
        catch (System.Exception e)
        {
            _logger.LogError(e, "Exception occurred: {Message}", e.Message);
            await Handle(context);
        }
    }

    private async Task Handle(
        HttpContext context,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError,
        string message = "Internal server error")
    {
        var problemDetails = new ProblemDetails
        {
            Status = (int)statusCode,
            Title = message
        };
        
        context.Response.StatusCode = (int)problemDetails.Status;
        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}