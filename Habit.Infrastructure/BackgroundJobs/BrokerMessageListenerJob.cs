using System.Text;
using Infrastructure.BackgroundJobs.Interfaces;
using Infrastructure.BrokerMessage;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Infrastructure.BackgroundJobs;

public class BrokerMessageListenerJob : IBrokerMessageListenerJob
{
    private readonly BrokerMessageSettings _settings;
    private readonly IModel _channel;

    public BrokerMessageListenerJob(IOptions<BrokerMessageSettings> options)
    {
        _settings = options.Value;

        var connectionFactory = new ConnectionFactory()
        {
            HostName = _settings.HostName,
            Port = _settings.Port,
            UserName = _settings.UserName,
            Password = _settings.Password,
            VirtualHost = _settings.VirtualHost
        };

        var connection = connectionFactory.CreateConnection();
        _channel = connection.CreateModel();
    }
    
    public void StartListening()
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += Consume;

        _channel.BasicConsume(_settings.Queue, false, consumer);
    }

    private void Consume(object? _, BasicDeliverEventArgs eventArgs)
    {
        var content = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

        Console.WriteLine($"Message: {content}");
        
        _channel.BasicAck(eventArgs.DeliveryTag, false);
    }
}