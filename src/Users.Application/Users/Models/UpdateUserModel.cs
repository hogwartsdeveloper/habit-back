namespace Users.Application.Users.Models;

/// <summary>
/// Модель для обновления информации о пользователе.
/// </summary>
public class UpdateUserModel
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
    /// Получает или устанавливает дату рождения пользователя.
    /// </summary>
    public DateTime? BirthDay { get; set; }
}