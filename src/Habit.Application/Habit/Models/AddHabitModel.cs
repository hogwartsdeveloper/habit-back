namespace Habit.Application.Habit.Models;

/// <summary>
/// Модель для добавления новой привычки.
/// </summary>
public class AddHabitModel
{
    /// <summary>
    /// Название привычки.
    /// </summary>
    public required string Title { get; set; }
    
    /// <summary>
    /// Описание привычки.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Дата начала привычки.
    /// </summary>
    public required DateTime StartDate { get; set; }
    
    /// <summary>
    /// Дата окончания привычки.
    /// </summary>
    public required DateTime EndDate { get; set; }
}
