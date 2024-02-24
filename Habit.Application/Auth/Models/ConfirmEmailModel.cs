namespace Habit.Application.Auth.Models;

/// <summary>
/// Модель подтверждения email.
/// </summary>
public class ConfirmEmailModel
{
    /// <summary>
    /// Получает или устанавливает email пользователя.
    /// </summary>
    public required string Email { get; set; }
    
    /// <summary>
    /// Получает или устанавливает код подтверждения.
    /// </summary>
    public required string Code { get; set; }
}
