using System.Net;

namespace Habit.Domain.Exceptions;

/// <summary>
/// Исключение, связанное с HTTP-запросом.
/// </summary>
public class HttpException : Exception
{
    /// <summary>
    /// Инициализирует новый экземпляр класса HttpException с указанным статусным кодом и сообщением об ошибке.
    /// </summary>
    /// <param name="statusCode">Статусный код HTTP.</param>
    /// <param name="message">Сообщение об ошибке.</param>
    public HttpException(HttpStatusCode statusCode, string message)
        : base(message)
    {
        StatusCode = statusCode;
    }
    
    /// <summary>
    /// Возвращает или устанавливает статусный код HTTP.
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }
}
