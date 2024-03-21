namespace Habit.Application.Auth.Models;

/// <summary>
/// Модель восстановления пароля.
/// </summary>
public class RecoveryPasswordModel
{
    /// <summary>
    /// Получает или устанавливает email пользователя.
    /// </summary>
    public required string Email { get; set; }
    
    /// <summary>
    /// Получает или устанавливает код восстановления.
    /// </summary>
    public required string Code { get; set; }
    
    /// <summary>
    /// Получает или устанавливает новый пароль пользователя.
    /// </summary>
    public required string Password { get; set; }
    
    /// <summary>
    /// Получает или устанавливает подтверждение нового пароля пользователя.
    /// </summary>
    public required string ConfirmPassword { get; set; }
}