using Habits.Application.Models;

namespace Habits.Application.Interfaces;

/// <summary>
/// Сервис привычек.
/// </summary>
public interface IHabitService
{
    /// <summary>
    /// Добавляет новую привычку для указанного пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="model">Модель добавления привычки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор добавленной привычки.</returns>
    Task<Guid> AddAsync(Guid userId, AddHabitModel model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает список всех привычек.
    /// </summary>
    /// /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список моделей привычек.</returns>
    Task<List<ViewHabitModel>> GetListAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает список привычек, сгруппированных.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список моделей привычек, сгруппированных.</returns>
    Task<ViewHabitGroupModel> GetListGroupAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает привычку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор привычки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель привычки или null, если привычка не найдена.</returns>
    Task<ViewHabitModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет информацию о привычке.
    /// </summary>
    /// <param name="id">Идентификатор привычки.</param>
    /// <param name="model">Модель обновления привычки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task UpdateAsync(Guid id, UpdateHabitModel model, CancellationToken cancellationToken = default);

    /// <summary>
    /// Добавляет записи о выполнении привычки.
    /// </summary>
    /// <param name="habitId">Идентификатор привычки.</param>
    /// <param name="models">Список моделей записей о выполнении привычки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task AddRecord(Guid habitId, List<ViewHabitRecordModel> models, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет привычку с указанным идентификатором.
    /// </summary>
    /// <param name="habitId">Идентификатор привычки для удаления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая операцию удаления.</returns>
    Task DeleteAsync(Guid habitId, CancellationToken cancellationToken = default);
}