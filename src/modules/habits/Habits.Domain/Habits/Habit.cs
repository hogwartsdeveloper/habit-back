

using BuildingBlocks.Entity.Abstraction;

namespace Habits.Domain.Habits;

/// <summary>
/// Класс, представляющий привычку пользователя.
/// </summary>
public class Habit : EntityBase
{
    private Habit() {}
    
    /// <summary>
    /// Инициализирует новый экземпляр класса Habit с указанным названием, датой начала и датой окончания.
    /// </summary>
    /// <param name="title">Название привычки.</param>
    /// <param name="startDate">Дата начала привычки.</param>
    /// <param name="endDate">Дата окончания привычки.</param>
    public Habit(string title, DateTime startDate, DateTime endDate)
    {
        Title = title;
        StartDate = startDate;
        EndDate = endDate;
    }

    /// <summary>
    /// Устанавливает идентификатор пользователя для привычки.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    public void SetUser(Guid userId)
    {
        UserId = userId;
    }

    /// <summary>
    /// Устанавливает флаг просроченности привычки.
    /// </summary>
    /// <param name="isOverdue">Флаг просроченности.</param>
    public void SetOverdue(bool isOverdue)
    {
        IsOverdue = isOverdue;
    }
    
    /// <summary>
    /// Возвращает идентификатор пользователя привычки.
    /// </summary>
    public Guid UserId { get; private set; }
    
    /// <summary>
    /// Возвращает название привычки.
    /// </summary>
    public string Title { get; private set; }
    
    /// <summary>
    /// Возвращает или устанавливает описание привычки.
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Возвращает флаг просроченности привычки.
    /// </summary>
    public bool IsOverdue { get; private set; }
    
    /// <summary>
    /// Возвращает дату начала привычки.
    /// </summary>
    public DateTime StartDate { get; private set; }
    
    /// <summary>
    /// Возвращает дату окончания привычки.
    /// </summary>
    public DateTime EndDate { get; private set; }
    
    /// <summary>
    /// Возвращает или устанавливает список записей привычки.
    /// </summary>
    public List<HabitRecord>? HabitRecords { get; private set; }
}