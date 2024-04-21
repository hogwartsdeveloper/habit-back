using System.Net;

namespace BuildingBlocks.Errors.Exceptions;

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

    /// <summary>
    /// Получает словарь тегов, связанных с ошибкой. Если теги не были предоставлены, возвращается пустой словарь.
    /// </summary>
    public Dictionary<string, string> Tags { get; private set; } = tags ?? new Dictionary<string, string>();
}