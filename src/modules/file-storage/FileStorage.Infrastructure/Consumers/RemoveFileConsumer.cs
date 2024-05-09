using BuildingBlocks.IntegrationEvents.Events;
using FileStorage.Application.FileStorage.Interfaces;
using MassTransit;

namespace FileStorage.Infrastructure.Consumers;

/// <summary>
/// Потребитель события удаления файла, который обрабатывает запросы на удаление файлов из хранилища.
/// </summary>
/// <param name="fileStorageService">Сервис для управления файлами в хранилище.</param>
public class RemoveFileConsumer(
    IFileStorageService fileStorageService) : IConsumer<IRemoveFileEvent>
{
    /// <summary>
    /// Обрабатывает событие удаления файла, вызывая метод удаления файла из сервиса хранения файлов.
    /// </summary>
    /// <param name="context">Контекст потребления события, содержащий данные о файле для удаления.</param>
    public async Task Consume(ConsumeContext<IRemoveFileEvent> context)
    {
        var message = context.Message;
        await fileStorageService.RemoveAsync(message.BucketName, message.FileName);
    }
}