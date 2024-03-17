using System.Security.Claims;
using Habit.Api.Controllers.Abstractions;
using Habit.Application.Auth.Interfaces;
using Habit.Application.Auth.Models;
using Habit.Application.Results;
using Habit.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Habit.Api.Controllers;

/// <summary>
/// Контроллер для аутентификации и авторизации пользователей.
/// </summary>
public class AuthController(IAuthService authService) : BaseController
{
    /// <summary>
    /// Регистрация нового пользователя.
    /// </summary>
    /// <param name="viewModel">Модель данных для регистрации.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель данных аутентификации.</returns>
    [HttpPost("SignUp")]
    [ProducesResponseType(typeof(ApiResult<AuthViewModel>), 200)]
    public Task<ApiResult<AuthViewModel>> SignUp([FromBody] RegistrationModel viewModel, CancellationToken cancellationToken)
    {
        return authService.SignUpAsync(viewModel, cancellationToken);
    }
    
    /// <summary>
    /// Вход пользователя в систему.
    /// </summary>
    /// <param name="viewModel">Модель данных для входа.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель данных аутентификации.</returns>
    [HttpPost("SignIn")]
    [ProducesResponseType(typeof(ApiResult<AuthViewModel>), 200)]
    public Task<ApiResult<AuthViewModel>> SignIn([FromBody] LoginModel viewModel, CancellationToken cancellationToken)
    {
        return authService.SignInAsync(viewModel, cancellationToken);
    }

    /// <summary>
    /// Обновление сессии пользователя.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель данных аутентификации.</returns>
    [Authorize]
    [HttpGet("Refresh")]
    [ProducesResponseType(typeof(ApiResult<AuthViewModel>), 200)]
    public Task<ApiResult<AuthViewModel>> Refresh(CancellationToken cancellationToken)
    {
        var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        return authService.RefreshSessionAsync(userEmail!, cancellationToken);
    }

    /// <summary>
    /// Подтверждение электронной почты пользователя.
    /// </summary>
    /// <param name="model">Модель данных для подтверждения почты.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Код состояния HTTP.</returns>
    [HttpPost("ConfirmEmail")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    public Task<ApiResult> ConfirmEmailAsync([FromBody] ConfirmEmailModel model, CancellationToken cancellationToken)
    {
        return authService.ConfirmEmailAsync(model, cancellationToken);
    }

    /// <summary>
    /// Запрос на восстановление пароля пользователя.
    /// </summary>
    /// <param name="model">Модель данных для запроса восстановления пароля.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Код состояния HTTP.</returns>
    [HttpPost("RequestForRecoveryPassword")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    public Task<ApiResult> RequestForRecoveryPassword([FromBody] RequestModel model, CancellationToken cancellationToken)
    {
        return authService.RequestForChangeAsync(model.Email, UserVerifyType.PasswordRecovery, cancellationToken);
    }

    /// <summary>
    /// Восстановление пароля пользователя.
    /// </summary>
    /// <param name="model">Модель данных для восстановления пароля.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Код состояния HTTP.</returns>
    [HttpPost("RecoveryPassword")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    public Task<ApiResult> RecoveryPassword([FromBody] RecoveryPasswordModel model, CancellationToken cancellationToken)
    {
        return authService.RecoveryPasswordAsync(model, cancellationToken);
    }
}