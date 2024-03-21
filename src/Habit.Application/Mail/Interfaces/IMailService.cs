using Habit.Application.Mail.Models;

namespace Habit.Application.Mail.Interfaces;

/// <summary>
/// Сервис отправки почты.
/// </summary>
public interface IMailService
{
    /// <summary>
    /// Отправляет почтовое сообщение.
    /// </summary>
    /// <param name="mailData">Данные для отправки почты.</param>
    /// <returns>Возвращает true, если отправка прошла успешно, иначе false.</returns>
    bool SendMail(MailData mailData);
}