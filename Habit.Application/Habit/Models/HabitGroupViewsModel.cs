namespace Habit.Application.Habit.Models;

/// <summary>
/// Модель представления группы привычек.
/// </summary>
public class HabitGroupViewsModel
{
    /// <summary>
    /// Получает или устанавливает список активных привычек.
    /// </summary>
    public required List<HabitViewModel> Active { get; set; }
    
    /// <summary>
    /// Получает или устанавливает список просроченных привычек.
    /// </summary>
    public required List<HabitViewModel> Overdue { get; set; }
}