namespace Infrastructure.BrokerMessage;

public class BrokerMessageSettings
{
    public string HostName { get; set; } = "localhost";
    public int Port { get; set; }
    public string UserName { get; set; } = "guest";
    public string Password { get; set; } = "guest";
    public required string Exchange { get; set; }
    public required string Queue { get; set; }
    public string RoutingKey { get; set; } = "*";
    public string VirtualHost { get; set; } = "/";
}