namespace BuildingBlocks.IntegrationEvents.Events;

/// <summary>
/// Интерфейс для события отправки сообщения, описывающий необходимые данные для формирования электронного письма.
/// </summary>
public interface ISendMessageEvent
{
    /// <summary>
    /// Адрес электронной почты получателя сообщения.
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Имя получателя сообщения.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Тема сообщения.
    /// </summary>
    public string Subject { get; set; }
    
    /// <summary>
    /// Текст тела сообщения.
    /// </summary>
    public string Body { get; set; }
}