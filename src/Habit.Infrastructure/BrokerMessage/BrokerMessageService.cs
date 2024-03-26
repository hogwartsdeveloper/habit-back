using System.Text;
using System.Text.Json;
using Habit.Application.BrokerMessage;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Infrastructure.BrokerMessage;

/// <inheritdoc />
public class BrokerMessageService : IBrokerMessageService
{
    private readonly BrokerMessageSettings _settings;
    private readonly ConnectionFactory _connectionFactory;
    
    /// <summary>
    /// Инициализирует новый экземпляр класса BrokerMessageService.
    /// </summary>
    /// <param name="options">Настройки для подключения к брокеру сообщений.</param>
    public BrokerMessageService(IOptions<BrokerMessageSettings> options)
    {
        _settings = options.Value;
        
        _connectionFactory = new ConnectionFactory
        {
            HostName = _settings.HostName,
            Port = _settings.Port,
            UserName = _settings.UserName,
            Password = _settings.Password,
            VirtualHost = _settings.VirtualHost
        };
    }

    /// <inheritdoc />
    public void SendMessage(object obj)
    {
        var message = JsonSerializer.Serialize(obj);
        
        SendMessage(message);
    }

    /// <inheritdoc />
    public void SendMessage(string message)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var channel = connection.CreateModel();
        
        channel.ExchangeDeclare(_settings.Exchange, "direct", true);

        var body = Encoding.UTF8.GetBytes(message);
            
        channel.BasicPublish(
            exchange: _settings.Exchange,
            routingKey: _settings.RoutingKey,
            basicProperties: null,
            body);
    }
}