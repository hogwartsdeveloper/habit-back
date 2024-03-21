using Habit.Domain.Entities.Abstraction;

namespace Habit.Domain.Entities;

/// <summary>
/// Класс, представляющий запись о выполнении привычки.
/// </summary>
public class HabitRecord : EntityBase
{
    private HabitRecord() {}
    
    /// <summary>
    /// Инициализирует новый экземпляр класса HabitRecord с указанным идентификатором привычки и датой.
    /// </summary>
    /// <param name="habitId">Идентификатор привычки.</param>
    /// <param name="date">Дата записи.</param>
    public HabitRecord(Guid habitId, DateTime date)
    {
        HabitId = habitId;
        Date = date;
    }
    
    /// <summary>
    /// Устанавливает идентификатор привычки для записи.
    /// </summary>
    /// <param name="habitId">Идентификатор привычки.</param>
    public void SetHabit(Guid habitId)
    {
        HabitId = habitId;
    }
    
    /// <summary>
    /// Возвращает идентификатор привычки, связанный с записью.
    /// </summary>
    public Guid HabitId { get; private set; }
    
    /// <summary>
    /// Возвращает привычку, связанную с записью.
    /// </summary>
    public Habit? Habit { get; private set; }
    
    /// <summary>
    /// Возвращает дату записи.
    /// </summary>
    public DateTime Date { get; private set; }
    
    /// <summary>
    /// Возвращает или устанавливает флаг завершённости привычки в эту дату.
    /// </summary>
    public bool IsComplete { get; private set; }
}