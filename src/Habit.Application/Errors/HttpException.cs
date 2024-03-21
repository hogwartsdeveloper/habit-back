using System.Net;

namespace Habit.Application.Errors;

/// <summary>
/// Исключение, связанное с HTTP-запросом.
/// </summary>
public class HttpException(
    HttpStatusCode statusCode,
    string message,
    Dictionary<string, string>? tags = null)
    : Exception(message)
{
    /// <summary>
    /// Возвращает или устанавливает статусный код HTTP.
    /// </summary>
    public HttpStatusCode StatusCode { get; private set; } = statusCode;

    public Dictionary<string, string> Tags { get; private set; } = tags ?? new Dictionary<string, string>();
}
