namespace Habit.Application.Auth.Models;

/// <summary>
/// Модель запроса.
/// </summary>
public class RequestModel
{
    /// <summary>
    /// Получает или устанавливает email.
    /// </summary>
    public required string Email { get; set; }
}