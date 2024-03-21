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

    /// <summary>
    /// Электронная почта обязательна для заполнения.
    /// </summary>
    public const string EmailIsRequired = "Электронная почта обязательна для заполнения.";

    /// <summary>
    /// Неверный адрес электронной почты
    /// </summary>
    public const string EmailIsInvalid = "Неверный адрес электронной почты";

    /// <summary>
    /// Код обязателен для заполнения.
    /// </summary>
    public const string CodeIsRequired = "Код обязателен для заполнения.";

    /// <summary>
    /// Код должен состоять из 4 символов.
    /// </summary>
    public const string CodeLength = "Код должен состоять из 4 символов.";

    /// <summary>
    /// Пароль обязателен для заполнения.
    /// </summary>
    public const string PasswordIsRequired = "Пароль обязателен для заполнения.";

    /// <summary>
    /// Пароль должен быть длиной не менее 5 символов.
    /// </summary>
    public const string PasswordLength = "Пароль должен быть длиной не менее 5 символов.";

    /// <summary>
    /// Пароль должен содержать как минимум одну цифру.
    /// </summary>
    public const string PasswordMustOneNumber = "Пароль должен содержать как минимум одну цифру.";

    /// <summary>
    /// Подтверждение пароля обязательно для заполнения.
    /// </summary>
    public const string ConfirmPasswordIsRequired = "Подтверждение пароля обязательно для заполнения.";

    /// <summary>
    /// Подтверждение пароля должно совпадать с паролем.
    /// </summary>
    public const string ConfirmPasswordMatchPassword = "Подтверждение пароля должно совпадать с паролем.";

    /// <summary>
    /// Имя обязательно для заполнения.
    /// </summary>
    public const string FirstNameIsRequired = "Имя обязательно для заполнения.";

    /// <summary>
    /// Фамилия обязательна для заполнения.
    /// </summary>
    public const string LastNameIsRequired = "Фамилия обязательна для заполнения.";

    public const string Unauthorized = "Не авторизован!";
}