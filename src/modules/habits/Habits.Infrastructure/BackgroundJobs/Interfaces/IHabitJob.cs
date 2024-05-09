namespace Habits.Infrastructure.BackgroundJobs.Interfaces;

/// <summary>
/// Интерфейс для работы с задачами связанными с привычками.
/// </summary>
public interface IHabitJob
{
    /// <summary>
    /// Метод для проверки просроченных привычек асинхронно.
    /// </summary>
    /// <returns>Асинхронная задача.</returns>
    public Task CheckIsOverdueAsync();
}