namespace Notifications.Application.Mail.Models;

/// <summary>
/// Модель настройки для отправки почты.
/// </summary>
public class MailSettingsModel
{
    /// <summary>
    /// SMTP-сервер для отправки почты.
    /// </summary>
    public required string Server { get; set; }
    
    /// <summary>
    /// Порт SMTP-сервера.
    /// </summary>
    public required int Port { get; set; }
    
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