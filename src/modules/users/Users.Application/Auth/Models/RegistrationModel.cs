namespace Users.Application.Auth.Models;

/// <summary>
/// Модель регистрации пользователя.
/// </summary>
public class RegistrationModel
{
    /// <summary>
    /// Получает или устанавливает имя пользователя.
    /// </summary>
    public required string FirstName { get; set; }
    
    /// <summary>
    /// Получает или устанавливает фамилию пользователя.
    /// </summary>
    public required string LastName { get; set; }
    
    /// <summary>
    /// Получает или устанавливает email пользователя.
    /// </summary>
    public required string Email { get; set; }
    
    /// <summary>
    /// Получает или устанавливает день рождения пользователя.
    /// </summary>
    public DateTime? BirthDay { get; set; }
    
    /// <summary>
    /// Получает или устанавливает пароль пользователя.
    /// </summary>
    public required string Password { get; set; }
}