using System.Net;
using Habit.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Habit.Api.Middleware;

/// <summary>
/// Промежуточное ПО для обработки исключений.
/// </summary>
public class ExceptionHandleMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandleMiddleware> _logger;

    /// <summary>
    /// Инициализирует новый экземпляр класса ExceptionHandleMiddleware.
    /// </summary>
    /// <param name="next">Следующий делегат запроса.</param>
    /// <param name="logger">Логгер.</param>
    public ExceptionHandleMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandleMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Вызывает промежуточное ПО для обработки запроса.
    /// </summary>
    /// <param name="context">Контекст HTTP-запроса.</param>
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
        catch (Exception e)
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