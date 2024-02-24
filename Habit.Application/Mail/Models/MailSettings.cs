namespace Habit.Application.Mail.Models;

/// <summary>
/// Настройки для отправки почты.
/// </summary>
public class MailSettings
{
    /// <summary>
    /// SMTP-сервер для отправки почты.
    /// </summary>
    public required string Server { get; set; }
    
    /// <summary>
    /// Порт SMTP-сервера.
    /// </summary>
    public int Port { get; set; }
    
    /// <summary>
    /// Имя отправителя.
    /// </summary>
    public required string SenderName { get; set; }
    
    /// <summary>
    /// Адрес электронной почты отправителя.
    /// </summary>
    public required string SenderEmail { get; set; }
    
    /// <summary>
    /// Имя пользователя для аутентификации на SMTP-сервере.
    /// </summary>
    public required string UserName { get; set; }
    
    /// <summary>
    /// Пароль для аутентификации на SMTP-сервере.
    /// </summary>
    public required string Password { get; set; }
}
