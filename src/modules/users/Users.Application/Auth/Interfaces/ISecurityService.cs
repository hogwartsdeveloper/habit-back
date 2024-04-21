using Users.Application.Auth.Models;
using Users.Domain.Users;

namespace Users.Application.Auth.Interfaces;

/// <summary>
/// Сервис безопасности.
/// </summary>
public interface ISecurityService
{
    /// <summary>
    /// Генерирует токен для пользователя.
    /// </summary>
    /// <param name="user">Пользователь.</param>
    /// <returns>Сгенерированный токен.</returns>
    string GenerateToken(User user);
    
    /// <summary>
    /// Генерирует токен обновления.
    /// </summary>
    /// <returns>Модель токена обновления.</returns>
    RefreshTokenModel GenerateRefreshToken();

    /// <summary>
    /// Хэширует пароль.
    /// </summary>
    /// <param name="password">Пароль.</param>
    /// <returns>Хэш пароля.</returns>
    string HashPassword(string password);

    /// <summary>
    /// Проверяет соответствие пароля и его хэша.
    /// </summary>
    /// <param name="password">Пароль.</param>
    /// <param name="passwordHash">Хэш пароля.</param>
    /// <returns>True, если пароль соответствует хэшу, иначе - false.</returns>
    bool VerifyPassword(string password, string passwordHash);

    /// <summary>
    /// Проверяет валидность токена обновления.
    /// </summary>
    /// <param name="model">Модель токена обновления.</param>
    /// <returns>True, если токен обновления валиден, иначе - false.</returns>
    bool VerifyRefreshToken(RefreshTokenModel model);
}