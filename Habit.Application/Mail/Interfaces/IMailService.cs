using Habit.Application.Mail.Models;

namespace Habit.Application.Mail.Interfaces;

public interface IMailService
{
    bool SendMail(MailData mailData);
}