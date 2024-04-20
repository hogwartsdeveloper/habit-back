namespace Habits.Application.Models;

/// <summary>
/// Модель представления группы привычек.
/// </summary>
public class ViewHabitGroupModel
{
    /// <summary>
    /// Получает или устанавливает список активных привычек.
    /// </summary>
    public required List<ViewHabitModel> Active { get; set; }
    
    /// <summary>
    /// Получает или устанавливает список просроченных привычек.
    /// </summary>
    public required List<ViewHabitModel> Overdue { get; set; }
}