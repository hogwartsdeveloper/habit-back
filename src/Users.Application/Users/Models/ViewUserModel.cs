namespace Users.Application.Users.Models;

/// <summary>
/// Модель представления для данных пользователя.
/// </summary>
public class ViewUserModel
{
    /// <summary>
    /// Получает имя пользователя.
    /// </summary>
    public required string FirstName { get; set; }
    
    /// <summary>
    /// Получает фамилию пользователя.
    /// </summary>
    public required string LastName { get; set; }
    
    /// <summary>
    /// Получает адрес электронной почты пользователя.
    /// </summary>
    public required string Email { get; set; }
    
    /// <summary>
    /// Получает подтверждение адреса электронной почты пользователя.
    /// </summary>
    public bool IsEmailConfirmed { get; set; }
    
    /// <summary>
    /// Получает дату рождения пользователя.
    /// </summary>
    public DateTime? BirthDay { get; set; }
    
    /// <summary>
    /// Получает URL изображения пользователя.
    /// </summary>
    public string? ImageUrl { get; set; }
}