namespace Habit.Application.Auth.Models;

/// <summary>
/// Модель представления аутентификации.
/// </summary>
public class AuthViewModel
{
    /// <summary>
    /// Получает или устанавливает токен доступа.
    /// </summary>
    public required string AccessToken { get; set; }
}