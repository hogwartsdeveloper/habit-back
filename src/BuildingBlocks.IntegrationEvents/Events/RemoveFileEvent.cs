namespace BuildingBlocks.IntegrationEvents.Events;

/// <summary>
/// Класс события для удаления файла.
/// </summary>
public class RemoveFileEvent
{
    /// <summary>
    /// Название хранилища, из которого нужно удалить файл.
    /// </summary>
    public required string BucketName { get; set; }
    
    /// <summary>
    /// Имя файла, который нужно удалить.
    /// </summary>
    public required string FileName { get; set; }
}