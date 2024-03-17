using Habit.Api.Controllers.Abstractions;
using Habit.Application.FileStorage.Models;
using Habit.Application.Results;
using Habit.Application.Users.Interfaces;
using Habit.Application.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Habit.Api.Controllers;

/// <summary>
/// Контроллер для управления пользователями.
/// </summary>
[Authorize]
public class UserController(IUserService service) : BaseController
{
    /// <summary>
    /// Добавляет изображение пользователю.
    /// </summary>
    /// <param name="model">Модель файла изображения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Результат операции.</returns>
    [HttpPost("Image")]
    [ProducesResponseType(typeof(ApiResult<>), 200)]
    public Task AddImage([FromForm] FileModel model, CancellationToken cancellationToken)
    {
        return service.AddImageAsync(GetCurrentUserId()!.Value, model.File, cancellationToken);
    }
    
    /// <summary>
    /// Удаляет изображение с указанным именем файла.
    /// </summary>
    /// <param name="fileName">Имя файла изображения для удаления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая операцию удаления изображения.</returns>
    [HttpDelete("Image")]
    [ProducesResponseType(typeof(ApiResult<>), 200)]
    public Task DeleteImage([FromQuery] string fileName, CancellationToken cancellationToken)
    {
        return service.DeleteImageAsync(GetCurrentUserId()!.Value, fileName, cancellationToken);
    }
    
    /// <summary>
    /// Обновляет информацию о пользователе.
    /// </summary>
    /// <param name="model">Модель для обновления информации о пользователе.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Результат операции.</returns>
    [HttpPut]
    [ProducesResponseType(typeof(ApiResult<>), 200)]
    public Task UpdateAsync([FromBody] UpdateUserModel model, CancellationToken cancellationToken)
    {
        return service.UpdateAsync(GetCurrentUserId()!.Value, model, cancellationToken);
    }

    /// <summary>
    /// Обновляет адрес электронной почты асинхронно.
    /// </summary>
    /// <param name="model">Модель обновления адреса электронной почты.</param>
    /// <param name="cancellationToken">Токен отмены для отмены операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    [HttpPut("UpdateEmail")]
    [ProducesResponseType(typeof(ApiResult<>), 200)]
    public Task UpdateEmailAsync([FromBody] UpdateEmailModel model, CancellationToken cancellationToken)
    {
        return service.UpdateEmailAsync(GetCurrentUserId()!.Value, model, cancellationToken);
    }

    /// <summary>
    /// Получает данные пользователя асинхронно по идентификатору.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая получение данных пользователя.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResult<UserViewModel>), 200)]
    public Task<UserViewModel> GetByIdAsync(CancellationToken cancellationToken)
    {
        return service.GetByIdAsync(GetCurrentUserId()!.Value, cancellationToken);
    }
}