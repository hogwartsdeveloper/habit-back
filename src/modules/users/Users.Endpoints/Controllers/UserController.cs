using BuildingBlocks.Endpoints.Abstraction;
using BuildingBlocks.Presentation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Users.Models;
using IUserService = Users.Application.Users.Interfaces.IUserService;

namespace Users.Endpoints.Controllers;

/// <summary>
/// Контроллер для управления пользователями.
/// </summary>
[Authorize]
public class UserController(IUserService service) : BaseController
{
    /// <summary>
    /// Получает данные пользователя асинхронно по идентификатору.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая получение данных пользователя.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResult<ViewUserModel>), 200)]
    public async Task<ApiResult<ViewUserModel>> GetByIdAsync(CancellationToken cancellationToken)
    {
        var result = await service.GetByIdAsync(GetCurrentUserId()!.Value, cancellationToken);
        
        return ApiResult<ViewUserModel>.Success(result);
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
}