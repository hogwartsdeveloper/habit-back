using Habit.Application.Users.Models;
using Microsoft.AspNetCore.Http;

namespace Habit.Application.Users.Interfaces;

/// <summary>
/// Сервис пользователя для выполнения операций с пользователями.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Добавляет изображение для пользователя асинхронно.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="file">Файл изображения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая добавление изображения.</returns>
    Task AddImageAsync(Guid id, IFormFile file, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Обновляет информацию о пользователе асинхронно.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="model">Модель с обновляемой информацией о пользователе.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая обновление пользователя.</returns>
    Task UpdateAsync(Guid id, UpdateUserModel model, CancellationToken cancellationToken = default);
}