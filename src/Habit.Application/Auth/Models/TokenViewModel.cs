using Habit.Domain.Entities;

namespace Habit.Application.Auth.Models;

/// <summary>
/// Модель представления токена.
/// </summary>
public class TokenViewModel
{
    /// <summary>
    /// Получает или устанавливает токен доступа.
    /// </summary>
    public required string AccessToken { get; set; }
    
    /// <summary>
    /// Получает или устанавливает токен обновления.
    /// </summary>
    public required RefreshToken RefreshToken { get; set; }
}

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