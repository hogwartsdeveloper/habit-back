namespace Infrastructure.BackgroundJobs.Interfaces;

public interface IBrokerMessageListenerJob
{
    void StartListening();
}