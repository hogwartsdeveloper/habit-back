namespace Users.Domain.Auth.Enums;

/// <summary>
/// Перечисление, представляющее типы верификации пользователя.
/// </summary>
public enum UserVerifyType
{
    /// <summary>
    /// Верификация по электронной почте.
    /// </summary>
    Email = 1,
    
    /// <summary>
    /// Восстановление пароля.
    /// </summary>
    PasswordRecovery = 2
}