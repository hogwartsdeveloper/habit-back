namespace Habit.Application.Habit.Models;

/// <summary>
/// Модель представления привычки.
/// </summary>
public class HabitViewModel
{
    /// <summary>
    /// Идентификатор привычки.
    /// </summary>
    public Guid Id { get; set; }
    
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
    
    /// <summary>
    /// Список записей привычки.
    /// </summary>
    public List<HabitRecordViewModel>? Records { get; set; }
}