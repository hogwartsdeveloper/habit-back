namespace Users.Application.Auth.Models;

/// <summary>
/// Модель токена обновления.
/// </summary>
public record RefreshTokenModel
{
    /// <summary>
    /// Получает или устанавливает токен.
    /// </summary>
    public required string Token { get; set; }
    
    /// <summary>
    /// Получает или устанавливает время истечения токена.
    /// </summary>
    public required DateTime Expires { get; set; }
}