namespace Habits.Application.Models;

/// <summary>
/// Модель для обновления привычки.
/// </summary>
public class UpdateHabitModel
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
    /// Флаг просроченности привычки.
    /// </summary>
    public bool IsOverdue { get; set; }
    
    /// <summary>
    /// Дата начала привычки.
    /// </summary>
    public required DateTime StartDate { get; set; }
    
    /// <summary>
    /// Дата окончания привычки.
    /// </summary>
    public required DateTime EndDate { get; set; }
}