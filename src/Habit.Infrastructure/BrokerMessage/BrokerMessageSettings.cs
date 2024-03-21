namespace Infrastructure.BrokerMessage;

/// <summary>
/// Настройки для подключения к брокеру сообщений.
/// </summary>
public class BrokerMessageSettings
{
    /// <summary>
    /// Имя хоста брокера сообщений.
    /// </summary>
    public string HostName { get; set; } = "localhost";
    
    /// <summary>
    /// Порт подключения к брокеру сообщений.
    /// </summary>
    public int Port { get; set; }
    
    /// <summary>
    /// Имя пользователя для аутентификации на брокере сообщений.
    /// </summary>
    public string UserName { get; set; } = "guest";
    
    /// <summary>
    /// Пароль для аутентификации на брокере сообщений.
    /// </summary>
    public string Password { get; set; } = "guest";
    
    /// <summary>
    /// Обмен сообщениями для отправки сообщений.
    /// </summary>
    public required string Exchange { get; set; }
    
    /// <summary>
    /// Очередь для отправки и получения сообщений.
    /// </summary>
    public required string Queue { get; set; }
    
    /// <summary>
    /// Ключ маршрутизации для сообщений.
    /// </summary>
    public string RoutingKey { get; set; } = "*";
    
    /// <summary>
    /// Виртуальный хост для подключения к брокеру сообщений.
    /// </summary>
    public string VirtualHost { get; set; } = "/";
}
