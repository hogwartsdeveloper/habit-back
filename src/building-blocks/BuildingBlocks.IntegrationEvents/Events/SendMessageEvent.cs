namespace BuildingBlocks.IntegrationEvents.Events;

/// <summary>
/// 
/// </summary>
public class SendMessageEvent
{
    /// <summary>
    /// 
    /// </summary>
    public required string Email { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public required string Subject { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public required string Body { get; set; }
}