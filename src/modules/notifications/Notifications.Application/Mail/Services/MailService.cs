using System.Net;
using BuildingBlocks.Errors.Constants;
using BuildingBlocks.Errors.Exceptions;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Notifications.Application.Mail.Interfaces;
using Notifications.Application.Mail.Models;

namespace Notifications.Application.Mail.Services;

/// <inheritdoc />
public class MailService : IMailService
{
    private readonly MailSettingsModel _mailSettings;
    private readonly ILogger<MailService> _logger;

    public MailService(
        IOptions<MailSettingsModel> mailSettingsOptions,
        ILogger<MailService> logger)
    {
        _mailSettings = mailSettingsOptions.Value;
        _logger = logger;
    }
    
    /// <inheritdoc />
    public bool SendMail(MailDataModel mailData)
    {
        try
        {
            using var message = new MimeMessage();
            var emailFrom = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail);
            message.From.Add(emailFrom);

            var emailTo = new MailboxAddress(mailData.Name, mailData.Email);
            message.To.Add(emailTo);

            message.Subject = mailData.Subject;

            message.Body = new TextPart(TextFormat.Plain)
            {
                Text = mailData.Body
            };

            using var mailClient = new SmtpClient();
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
            throw new HttpException(HttpStatusCode.InternalServerError, ExceptionMessages.InternalServerError);
        }
    }
}