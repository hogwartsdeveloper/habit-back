using System.Security.Claims;
using BuildingBlocks.Presentation.Controllers.Abstraction;
using BuildingBlocks.Validation.Attributes;
using Habit.Application.Auth.Models;
using Habit.Application.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Auth.Interfaces;
using Users.Application.Auth.Models;
using Users.Domain.Auth.Enums;
using ConfirmEmailModel = Users.Application.Auth.Models.ConfirmEmailModel;
using LoginModel = Users.Application.Auth.Models.LoginModel;
using RecoveryPasswordModel = Users.Application.Auth.Models.RecoveryPasswordModel;
using RegistrationModel = Users.Application.Auth.Models.RegistrationModel;

namespace Users.Endpoints.Controllers;

/// <summary>
/// Контроллер для аутентификации и авторизации пользователей.
/// </summary>
[ValidateModel]
public class AuthController(IAuthService authService) : BaseController
{
    /// <summary>
    /// Регистрация нового пользователя.
    /// </summary>
    /// <param name="viewModel">Модель данных для регистрации.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель данных аутентификации.</returns>
    [HttpPost("SignUp")]
    [ProducesResponseType(typeof(ApiResult<ViewAuthModel>), 200)]
    public async Task<ApiResult<ViewAuthModel>> SignUp(
        [FromBody] RegistrationModel viewModel,
        CancellationToken cancellationToken)
    {
        var result = await authService.SignUpAsync(viewModel, cancellationToken);
        
        return ApiResult<ViewAuthModel>.Success(result);
    }
    
    /// <summary>
    /// Вход пользователя в систему.
    /// </summary>
    /// <param name="viewModel">Модель данных для входа.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель данных аутентификации.</returns>
    [HttpPost("SignIn")]
    [ProducesResponseType(typeof(ApiResult<ViewAuthModel>), 200)]
    public async Task<ApiResult<ViewAuthModel>> SignIn(
        [FromBody] LoginModel viewModel,
        CancellationToken cancellationToken)
    {
        var result = await authService.SignInAsync(viewModel, cancellationToken);
        
        return ApiResult<ViewAuthModel>.Success(result);
    }
    
    /// <summary>
    /// Обновление сессии пользователя.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель данных аутентификации.</returns>
    [Authorize]
    [HttpGet("Refresh")]
    [ProducesResponseType(typeof(ApiResult<ViewAuthModel>), 200)]
    public async Task<ApiResult<ViewAuthModel>> Refresh(CancellationToken cancellationToken)
    {
        var userEmail = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var result = await authService.RefreshSessionAsync(userEmail!, cancellationToken);

        return ApiResult<ViewAuthModel>.Success(result);
    }
    
    /// <summary>
    /// Подтверждение электронной почты пользователя.
    /// </summary>
    /// <param name="model">Модель данных для подтверждения почты.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Код состояния HTTP.</returns>
    [HttpPost("ConfirmEmail")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    public async Task<ApiResult> ConfirmEmailAsync(
        [FromBody] ConfirmEmailModel model,
        CancellationToken cancellationToken)
    {
        await authService.ConfirmEmailAsync(model, cancellationToken);
        return ApiResult.Success();
    }
    
    /// <summary>
    /// Запрос на восстановление пароля пользователя.
    /// </summary>
    /// <param name="model">Модель данных для запроса восстановления пароля.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Код состояния HTTP.</returns>
    [HttpPost("RequestForRecoveryPassword")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    public async Task<ApiResult> RequestForRecoveryPassword(
        [FromBody] RequestModel model,
        CancellationToken cancellationToken)
    {
        await authService.RequestForChangeAsync(model.Email, UserVerifyType.PasswordRecovery, cancellationToken);
        return ApiResult.Success();
    }
    
    /// <summary>
    /// Запрос на подтверждения почты.
    /// </summary>
    /// <param name="model">Модель данных для запроса восстановления пароля.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Код состояния HTTP.</returns>
    [HttpPost("RequestForVerifyEmail")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    public async Task<ApiResult> RequestForVerifyEmail(
        [FromBody] RequestModel model,
        CancellationToken cancellationToken)
    {
        await authService.RequestForChangeAsync(model.Email, UserVerifyType.Email, cancellationToken);
        return ApiResult.Success();
    }
    
    /// <summary>
    /// Восстановление пароля пользователя.
    /// </summary>
    /// <param name="model">Модель данных для восстановления пароля.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Код состояния HTTP.</returns>
    [HttpPost("RecoveryPassword")]
    [ProducesResponseType(typeof(ApiResult), 200)]
    public async Task<ApiResult> RecoveryPassword(
        [FromBody] RecoveryPasswordModel model,
        CancellationToken cancellationToken)
    {
        await authService.RecoveryPasswordAsync(model, cancellationToken);
        return ApiResult.Success();
    }
}