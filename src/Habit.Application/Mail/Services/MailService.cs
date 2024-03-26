using System.Net;
using Habit.Application.Constants;
using Habit.Application.Errors;
using Habit.Application.Mail.Interfaces;
using Habit.Application.Mail.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Habit.Application.Mail.Services;

/// <inheritdoc />
public class MailService : IMailService
{
    private readonly MailSettings _mailSettings;
    private readonly ILogger<MailService> _logger;

    public MailService(
        ILogger<MailService> logger,
        IOptions<MailSettings> mailSettingsOptions)
    {
        _logger = logger;
        _mailSettings = mailSettingsOptions.Value;
    }
    
    /// <inheritdoc />
    public bool SendMail(MailData mailData)
    {
        try
        {
            using MimeMessage message = new MimeMessage();
            var emailFrom = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail);
            message.From.Add(emailFrom);

            var emailTo = new MailboxAddress(mailData.Name, mailData.Email);
            message.To.Add(emailTo);

            message.Subject = mailData.Subject;

            message.Body = new TextPart(TextFormat.Plain)
            {
                Text = mailData.Body
            };

            using SmtpClient mailClient = new SmtpClient();
            mailClient.Connect(
                _mailSettings.Server,
                _mailSettings.Port,
                MailKit.Security.SecureSocketOptions.StartTls);
                    
            mailClient.Authenticate(_mailSettings.UserName, _mailSettings.Password);

            mailClient.Send(message);
            mailClient.Disconnect(true);

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при отправке сообщения в почту. Message: {1}", e.Message);
            throw new HttpException(HttpStatusCode.InternalServerError, ApplicationConstant.InternalServerError);
        }
    }
}