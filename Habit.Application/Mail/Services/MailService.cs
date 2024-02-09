using Habit.Application.Mail.Interfaces;
using Habit.Application.Mail.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Habit.Application.Mail.Services;

public class MailService : IMailService
{
    private readonly MailSettings _mailSettings;

    public MailService(IOptions<MailSettings> mailSettingsOptions)
    {
        _mailSettings = mailSettingsOptions.Value;
    }
    
    public bool SendMail(MailData mailData)
    {
        try
        {
            using (MimeMessage message = new MimeMessage())
            {
                MailboxAddress emailFrom = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail);
                message.From.Add(emailFrom);

                MailboxAddress emailTo = new MailboxAddress(mailData.Name, mailData.Email);
                message.To.Add(emailTo);

                message.Subject = mailData.Subject;

                message.Body = new TextPart(TextFormat.Plain)
                {
                    Text = mailData.Body
                };
                
                using (SmtpClient mailClient = new SmtpClient())
                {
                    mailClient.Connect(
                        _mailSettings.Server,
                        _mailSettings.Port,
                        MailKit.Security.SecureSocketOptions.StartTls);
                    
                    mailClient.Authenticate(_mailSettings.UserName, _mailSettings.Password);

                    mailClient.Send(message);
                    mailClient.Disconnect(true);
                }
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}