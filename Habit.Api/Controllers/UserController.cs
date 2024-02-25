using System.Security.Claims;
using Habit.Application.FileStorage.Models;
using Habit.Application.Users.Interfaces;
using Habit.Application.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Habit.Api.Controllers;

/// <summary>
/// Контроллер для управления пользователями.
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService service) : ControllerBase
{
    /// <summary>
    /// Добавляет изображение пользователю.
    /// </summary>
    /// <param name="model">Модель файла изображения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Результат операции.</returns>
    [HttpPost("image")]
    [ProducesResponseType(typeof(IResult), 200)]
    public Task AddImage([FromForm] FileModel model, CancellationToken cancellationToken)
    {
        var userIdData = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        Guid.TryParse(userIdData, out var userId);

        return service.AddImageAsync(userId, model.File, cancellationToken);
    }
    
    /// <summary>
    /// Обновляет информацию о пользователе.
    /// </summary>
    /// <param name="model">Модель для обновления информации о пользователе.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Результат операции.</returns>
    [HttpPut]
    [ProducesResponseType(typeof(IResult), 200)]
    public Task UpdateAsync([FromBody] UpdateUserModel model, CancellationToken cancellationToken)
    {
        var userIdData = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        Guid.TryParse(userIdData, out var userId);
        
        return service.UpdateAsync(userId, model, cancellationToken);
    }

    /// <summary>
    /// Обновляет адрес электронной почты асинхронно.
    /// </summary>
    /// <param name="model">Модель обновления адреса электронной почты.</param>
    /// <param name="cancellationToken">Токен отмены для отмены операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию.</returns>
    [HttpPut("UpdateEmail")]
    [ProducesResponseType(typeof(IResult), 200)]
    public Task UpdateEmailAsync([FromBody] UpdateEmailModel model, CancellationToken cancellationToken)
    {
        var userIdData = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        Guid.TryParse(userIdData, out var userId);

        return service.UpdateEmailAsync(userId, model, cancellationToken);
    }

    /// <summary>
    /// Получает данные пользователя асинхронно по идентификатору.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая получение данных пользователя.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(UserViewModel), 200)]
    public Task<UserViewModel> GetByIdAsync(CancellationToken cancellationToken)
    {
        var userIdData = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        Guid.TryParse(userIdData, out var userId);

        return service.GetByIdAsync(userId, cancellationToken);
    }
}