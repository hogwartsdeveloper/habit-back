namespace Habit.Domain.Entities.Abstraction;


/// <summary>
/// Базовый абстрактный класс, предоставляющий основную реализацию для сущностей.
/// </summary>
public abstract class EntityBase
{
    /// <summary>
    /// Получает или устанавливает уникальный идентификатор сущности.
    /// </summary>
    public Guid Id { get; set; }
}