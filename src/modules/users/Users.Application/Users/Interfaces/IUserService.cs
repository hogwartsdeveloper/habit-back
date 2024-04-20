using Users.Application.Users.Models;

namespace Users.Application.Users.Interfaces;

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
    /// Асинхронно удаляет изображение с указанным идентификатором и именем файла.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="fileName">Имя файла изображения для удаления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая операцию удаления изображения.</returns>
    Task DeleteImageAsync(Guid id, string fileName, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Обновляет информацию о пользователе асинхронно.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="model">Модель с обновляемой информацией о пользователе.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая обновление пользователя.</returns>
    Task UpdateAsync(Guid id, UpdateUserModel model, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Асинхронно обновляет адрес электронной почты.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="model">Модель для обновления адреса электронной почты.</param>
    /// <param name="cancellationToken">Токен отмены для отмены операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    Task UpdateEmailAsync(Guid id, UpdateEmailModel model, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Получает данные пользователя по его идентификатору асинхронно.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="viewModel">Модель представления пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая получение данных пользователя.</returns>
    Task<ViewUserModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}