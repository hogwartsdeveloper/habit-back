namespace BuildingBlocks.IntegrationEvents.Events;

/// <summary>
/// Определяет интерфейс для события удаления файла из хранилища.
/// </summary>
public interface IRemoveFileEvent
{
    /// <summary>
    /// Название хранилища, из которого нужно удалить файл.
    /// </summary>
    string BucketName { get; set; }
    
    /// <summary>
    /// Имя файла, который нужно удалить.
    /// </summary>
    string FileName { get; set; }
}