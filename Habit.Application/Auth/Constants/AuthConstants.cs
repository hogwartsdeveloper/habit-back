namespace Habit.Application.Auth.Constants;

/// <summary>
/// Константы для аутентификации.
/// </summary>
public static class AuthConstants
{
    /// <summary>
    /// Сообщение о необходимости подтверждения адреса электронной почты.
    /// </summary>
    public const string ConfirmEmail = "Подтвердите адрес электронной почты.";

    /// <summary>
    /// Сообщение о запросе на смену пароля.
    /// </summary>
    public const string RequestForChangePassword = "Запрос на смену пароля.";
    
    /// <summary>
    /// Сообщение об ошибке в адресе электронной почты или пароле.
    /// </summary>
    public const string EmailOrPasswordWrong = "Адрес электронной почты или пароль неправильный.";

    /// <summary>
    /// Сообщение о том, что пользователь не найден.
    /// </summary>
    public const string UserNotFound = "Пользователь не найден!";

    /// <summary>
    /// Сообщение о том, что пользователь уже существует.
    /// </summary>
    public const string UserAlreadyExists = "Пользователь уже существует.";

    /// <summary>
    /// Сообщение о недействительности токена.
    /// </summary>
    public const string TokenInvalid = "Токен недействителен.";

    /// <summary>
    /// Сообщение о том, что код неверен.
    /// </summary>
    public const string CodeIsNotCorrect = "Код неверен.";
}