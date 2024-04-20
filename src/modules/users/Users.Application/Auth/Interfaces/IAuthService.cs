using Users.Application.Auth.Models;
using Users.Domain.Auth.Enums;

namespace Users.Application.Auth.Interfaces;

/// <summary>
/// Интерфейс сервиса аутентификации.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Регистрация нового пользователя.
    /// </summary>
    /// <param name="model">Модель регистрации.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель аутентификации.</returns>
    Task<ViewAuthModel> SignUpAsync(RegistrationModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Вход пользователя.
    /// </summary>
    /// <param name="model">Модель входа.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель аутентификации.</returns>
    Task<ViewAuthModel> SignInAsync(LoginModel model, CancellationToken cancellationToken);
    
    /// <summary>
    /// Обновление сессии пользователя.
    /// </summary>
    /// <param name="email">Email пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель аутентификации.</returns>
    Task<ViewAuthModel> RefreshSessionAsync(string email, CancellationToken cancellationToken);

    /// <summary>
    /// Подтверждение email пользователя.
    /// </summary>
    /// <param name="model">Модель подтверждения email.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача.</returns>
    Task ConfirmEmailAsync(ConfirmEmailModel model, CancellationToken cancellationToken);

    /// <summary>
    /// Запрос на изменение данных пользователя.
    /// </summary>
    /// <param name="email">Email пользователя.</param>
    /// <param name="verifyType">Тип верификации.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача.</returns>
    Task RequestForChangeAsync(string email, UserVerifyType verifyType, CancellationToken cancellationToken);

    /// <summary>
    /// Восстановление пароля пользователя.
    /// </summary>
    /// <param name="model">Модель восстановления пароля.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача.</returns>
    Task RecoveryPasswordAsync(RecoveryPasswordModel model, CancellationToken cancellationToken);
}