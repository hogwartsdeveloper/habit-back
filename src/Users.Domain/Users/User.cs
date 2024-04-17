using Users.Domain.Auth;

namespace Users.Domain.Users;

/// <summary>
/// Представляет сущность пользователя.
/// </summary>
public class User
{
    private User() {}
    
    /// <summary>
    /// Инициализирует новый экземпляр класса User с указанными данными пользователя.
    /// </summary>
    /// <param name="firstName">Имя пользователя.</param>
    /// <param name="lastName">Фамилия пользователя.</param>
    /// <param name="email">Адрес электронной почты пользователя.</param>
    public User(
        string firstName,
        string lastName,
        string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }
    
    /// <summary>
    /// Регистрирует пользователя с указанным хэшем пароля.
    /// </summary>
    /// <param name="passwordHash">Хэш пароля пользователя.</param>
    public void Registration(string passwordHash)
    {
        PasswordHash = passwordHash;
        IsEmailConfirmed = false;
    }
    
    /// <summary>
    /// Подтверждает адрес электронной почты пользователя.
    /// </summary>
    public void ConfirmEmail()
    {
        IsEmailConfirmed = true;
    }
    
    /// <summary>
    /// Изменяет адрес электронной почты.
    /// </summary>
    /// <param name="email">Новый адрес электронной почты.</param>
    public void ChangeEmail(string email)
    {
        IsEmailConfirmed = false;
        Email = email;
    }
    
    /// <summary>
    /// Изменяет хэш пароля пользователя.
    /// </summary>
    /// <param name="passwordHash">Новый хэш пароля пользователя.</param>
    public void ChangePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
    }
    
    /// <summary>
    /// Изменяет дату рождения пользователя.
    /// </summary>
    /// <param name="birthDay">Новая дата рождения пользователя.</param>
    public void ChangeBirthDay(DateTime? birthDay)
    {
        if (birthDay is not null)
        {
            BirthDay = birthDay;
        }
    }
    
    /// <summary>
    /// Изменяет URL изображения пользователя.
    /// </summary>
    /// <param name="imageUrl">Новый URL изображения пользователя.</param>
    public void ChangeImage(string imageUrl)
    {
        ImageUrl = imageUrl;
    }
    
    /// <summary>
    /// Сбрасывает ссылку на изображение, устанавливая её в значение null.
    /// </summary>
    public void DeleteImage()
    {
        ImageUrl = null;
    }
    
    /// <summary>
    /// Получает имя пользователя.
    /// </summary>
    public string FirstName { get; private set; }
    
    /// <summary>
    /// Получает фамилию пользователя.
    /// </summary>
    public string LastName { get; private set; }
    
    /// <summary>
    /// Получает адрес электронной почты пользователя.
    /// </summary>
    public string Email { get; private set; }
    
    /// <summary>
    /// Получает признак подтверждения адреса электронной почты пользователя.
    /// </summary>
    public bool IsEmailConfirmed { get; private set; }
    
    /// <summary>
    /// Получает дату рождения пользователя.
    /// </summary>
    public DateTime? BirthDay { get; private set; }
    
    /// <summary>
    /// Получает хэш пароля пользователя.
    /// </summary>
    public string PasswordHash { get; private set; }
    
    /// <summary>
    /// Получает URL изображения пользователя.
    /// </summary>
    public string? ImageUrl { get; private set; }
    
    /// <summary>
    /// Получает список подтверждений пользователя.
    /// </summary>
    public List<UserVerify>? UserVerification { get; private set; }
    
    /// <summary>
    /// Получает токен обновления пользователя.
    /// </summary>
    public RefreshToken? RefreshToken { get; private set; }
}