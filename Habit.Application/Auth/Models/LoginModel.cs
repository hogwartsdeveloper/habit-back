namespace Habit.Application.Auth.Models;

/// <summary>
/// Модель входа пользователя.
/// </summary>
public class LoginModel
{
    /// <summary>
    /// Получает или устанавливает email пользователя.
    /// </summary>
    public required string Email { get; set; }
    
    /// <summary>
    /// Получает или устанавливает пароль пользователя.
    /// </summary>
    public required string Password { get; set; }
}
