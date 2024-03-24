using System.Text;
using Habit.Application.Mail.Interfaces;
using Habit.Application.Mail.Models;
using Infrastructure.BackgroundJobs.Interfaces;
using Infrastructure.BrokerMessage;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Infrastructure.BackgroundJobs;

/// <inheritdoc />
public class BrokerMessageListenerJob : IBrokerMessageListenerJob
{
    private readonly BrokerMessageSettings _settings;
    private readonly ConnectionFactory _connectionFactory;
    private readonly IMailService _mailService;

    /// <summary>
    /// Конструктор для создания экземпляра слушателя сообщений брокера.
    /// </summary>
    /// <param name="options">Настройки сообщений брокера.</param>
    /// <param name="mailService">Сервис отправки почты.</param>
    public BrokerMessageListenerJob(IOptions<BrokerMessageSettings> options, IMailService mailService)
    {
        _settings = options.Value;
        _mailService = mailService;

        _connectionFactory = new ConnectionFactory()
        {
            HostName = _settings.HostName,
            Port = _settings.Port,
            UserName = _settings.UserName,
            Password = _settings.Password,
            VirtualHost = _settings.VirtualHost,
        };
    }

    /// <inheritdoc />
    public void UserVerifyStartListening()
    {
        using var connection = _connectionFactory.CreateConnection();
        using var channel = connection.CreateModel();
        var consumer = new EventingBasicConsumer(channel);
        
        consumer.Received += (model, ea) =>
        {
            UserVerifyConsume(model, ea);
            channel.BasicAck(ea.DeliveryTag, false);
        };
        
        channel.QueueDeclare(queue: _settings.Queue, true, false, false, null);
        channel.ExchangeDeclare(_settings.Exchange, "topic", false, false, null);
        channel.QueueBind(_settings.Queue, _settings.Exchange, _settings.RoutingKey, null);
        channel.BasicConsume(_settings.Queue, false, consumer);
    }

    private void UserVerifyConsume(object? _, BasicDeliverEventArgs eventArgs)
    {
        var content = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

        var mailData = JsonSerializer.Deserialize<MailData>(content);

        if (mailData is not null)
        {
            _mailService.SendMail(mailData);
        }
    }
}