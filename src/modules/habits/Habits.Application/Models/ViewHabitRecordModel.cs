namespace Habits.Application.Models;

/// <summary>
/// Модель представления записи привычки.
/// </summary>
public class ViewHabitRecordModel
{
    /// <summary>
    /// Дата выполнения привычки.
    /// </summary>
    public required DateTime Date { get; set; }
    
    /// <summary>
    /// Флаг завершенности привычки.
    /// </summary>
    public bool IsComplete { get; set; }
}