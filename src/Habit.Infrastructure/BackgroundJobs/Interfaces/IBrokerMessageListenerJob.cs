namespace Infrastructure.BackgroundJobs.Interfaces;

/// <summary>
/// Интерфейс для слушателя сообщений брокера.
/// </summary>
public interface IBrokerMessageListenerJob
{
    /// <summary>
    /// Начать прослушивание сообщений о подтверждении пользователя.
    /// </summary>
    void UserVerifyStartListening();
}