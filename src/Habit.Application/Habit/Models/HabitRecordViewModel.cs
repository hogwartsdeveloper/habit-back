namespace Habit.Application.Habit.Models;

/// <summary>
/// Модель представления записи привычки.
/// </summary>
public class HabitRecordViewModel
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