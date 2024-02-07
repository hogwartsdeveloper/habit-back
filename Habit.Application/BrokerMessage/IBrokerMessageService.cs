namespace Habit.Application.BrokerMessage;

public interface IBrokerMessageService
{
    void SendMessage(object obj);
    void SendMessage(string message);
}