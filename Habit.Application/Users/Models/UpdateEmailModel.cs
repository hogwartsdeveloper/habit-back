namespace Habit.Application.Users.Models;

/// <summary>
/// Модель для обновления адреса электронной почты.
/// </summary>
public class UpdateEmailModel
{
    /// <summary>
    /// Получает или задает адрес электронной почты.
    /// </summary>
    public required string Email { get; set; }
}