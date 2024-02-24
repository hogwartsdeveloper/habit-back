namespace Habit.Application.BrokerMessage;

/// <summary>
/// Сервис для отправки сообщений через брокер.
/// </summary>
public interface IBrokerMessageService
{
    /// <summary>
    /// Отправляет сообщение объектом.
    /// </summary>
    /// <param name="obj">Объект сообщения.</param>
    void SendMessage(object obj);
    
    /// <summary>
    /// Отправляет сообщение строкой.
    /// </summary>
    /// <param name="message">Текст сообщения.</param>
    void SendMessage(string message);
}