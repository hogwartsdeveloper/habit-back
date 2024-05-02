namespace Notifications.Application.Mail.Models;

/// <summary>
/// Модель данных для отправки почты.
/// </summary>
public class MailDataModel
{
    /// <summary>
    /// Адрес электронной почты получателя.
    /// </summary>
    public required string Email { get; set; }
    
    /// <summary>
    /// Имя получателя.
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Тема письма.
    /// </summary>
    public required string Subject { get; set; }
    
    /// <summary>
    /// Текст сообщения.
    /// </summary>
    public required string Body { get; set; }
}