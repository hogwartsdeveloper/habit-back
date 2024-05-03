using BuildingBlocks.IntegrationEvents.Events;
using MassTransit;
using Notifications.Application.Mail.Interfaces;
using Notifications.Application.Mail.Models;

namespace Notifications.Infrastructure.Consumers;

/// <summary>
/// Потребитель события отправки сообщения, который обрабатывает запросы на отправку электронных писем.
/// </summary>
/// <param name="mailService">Сервис для отправки электронной почты.</param>
public class SendMessageConsumer(IMailService mailService) : IConsumer<ISendMessageEvent>
{
    /// <summary>
    /// Обрабатывает событие отправки сообщения,
    /// используя предоставленные данные для формирования и отправки электронной почты через сервис почты.
    /// </summary>
    /// <param name="context">Контекст потребления события, содержащий данные о сообщении для отправки.</param>
    /// <returns>Задача, представляющая асинхронное выполнение метода.</returns>
    public Task Consume(ConsumeContext<ISendMessageEvent> context)
    {
        var message = context.Message;

        mailService.SendMail(new MailDataModel
        {
            Email = message.Email,
            Name = message.Name,
            Subject = message.Subject,
            Body = message.Body
        });
        
        return Task.CompletedTask;
    }
}