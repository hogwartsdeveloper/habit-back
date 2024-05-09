namespace Users.Application.Auth.Models;

/// <summary>
/// Модель представления аутентификации.
/// </summary>
public class ViewAuthModel
{
    /// <summary>
    /// Получает или устанавливает токен доступа.
    /// </summary>
    public required string AccessToken { get; set; }
}