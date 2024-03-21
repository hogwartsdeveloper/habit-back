using System.Security.Claims;
using Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Habit.Api.Controllers.Abstractions;

/// <summary>
/// Базовый контроллер для API.
/// </summary>
[ApiController]
[ValidateModel]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    /// <summary>
    /// Получает идентификатор текущего пользователя.
    /// </summary>
    /// <returns>Идентификатор текущего пользователя или null, если пользователь не аутентифицирован.</returns>
    protected Guid? GetCurrentUserId()
    {
        var value = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        var isParse = Guid.TryParse(value, out var userId);

        if (isParse)
        {
            return userId;
        }

        return null;
    }
}