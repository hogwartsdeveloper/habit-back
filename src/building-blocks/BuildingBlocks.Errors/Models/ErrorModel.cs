namespace BuildingBlocks.Errors.Models;

/// <summary>
/// Представляет модель ошибки с кодом статуса, сообщением и набором тегов.
/// </summary>
/// <param name="statusCode">Код статуса ошибки.</param>
/// <param name="message">Сообщение ошибки.</param>
/// <param name="tags">Словарь тегов, связанных с ошибкой (необязательный).</param>
public class ErrorModel(
    int statusCode,
    string message,
    Dictionary<string, string>? tags = null)
{
    /// <summary>
    /// Получает код статуса ошибки.
    /// </summary>
    public int StatusCode { get; private set; } = statusCode;

    /// <summary>
    /// Получает сообщение об ошибке.
    /// </summary>
    public string Message { get; private set; } = message;

    /// <summary>
    /// Получает словарь тегов, связанных с ошибкой. Если теги не были предоставлены, возвращается пустой словарь.
    /// </summary>
    public Dictionary<string, string> Tags { get; private set; } = tags ?? new Dictionary<string, string>();
}