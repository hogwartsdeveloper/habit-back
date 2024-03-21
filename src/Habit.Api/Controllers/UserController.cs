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
    [ProducesResponseType(typeof(ApiResult), 200)]
    public async Task<ApiResult> AddImage([FromForm] FileModel model, CancellationToken cancellationToken)
    {
        await service.AddImageAsync(GetCurrentUserId()!.Value, model.File, cancellationToken);
        return ApiResult.Success();
    }
    
    /// <summary>
    /// Удаляет изображение с указанным именем файла.
    /// </summary>
    /// <param name="fileName">Имя файла изображения для удаления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая операцию удаления изображения.</returns>
    [HttpDelete("Image")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    public async Task<ApiResult> DeleteImage([FromQuery] string fileName, CancellationToken cancellationToken)
    {
        await service.DeleteImageAsync(GetCurrentUserId()!.Value, fileName, cancellationToken);
        return ApiResult.Success();
    }
    
    /// <summary>
    /// Обновляет информацию о пользователе.
    /// </summary>
    /// <param name="model">Модель для обновления информации о пользователе.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Результат операции.</returns>
    [HttpPut]
    [ProducesResponseType(typeof(ApiResult), 200)]
    public async Task<ApiResult> UpdateAsync([FromBody] UpdateUserModel model, CancellationToken cancellationToken)
    {
        await service.UpdateAsync(GetCurrentUserId()!.Value, model, cancellationToken);
        
        return ApiResult.Success();
    }

    /// <summary>
    /// Обновляет адрес электронной почты асинхронно.
    /// </summary>
    /// <param name="model">Модель обновления адреса электронной почты.</param>
    /// <param name="cancellationToken">Токен отмены для отмены операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    [HttpPut("UpdateEmail")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    public async Task<ApiResult> UpdateEmailAsync(
        [FromBody] UpdateEmailModel model,
        CancellationToken cancellationToken)
    {
        await service.UpdateEmailAsync(GetCurrentUserId()!.Value, model, cancellationToken);

        return ApiResult.Success();
    }

    /// <summary>
    /// Получает данные пользователя асинхронно по идентификатору.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая получение данных пользователя.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResult<UserViewModel>), 200)]
    public async Task<ApiResult<UserViewModel>> GetByIdAsync(CancellationToken cancellationToken)
    {
        var result = await service.GetByIdAsync(GetCurrentUserId()!.Value, cancellationToken);
        
        return ApiResult<UserViewModel>.Success(result);
    }
}