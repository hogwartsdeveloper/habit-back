namespace FileStorage.Infrastructure.Settings;

/// <summary>
/// Настройки для подключения к облачному хранилищу файлов.
/// </summary>
public class FileStorageSettings
{
    /// <summary>
    /// Конечная точка (endpoint) облачного хранилища.
    /// </summary>
    public required string Endpoint { get; set; }

    /// <summary>
    /// Ключ доступа для подключения к облачному хранилищу.
    /// </summary>
    public required string AccessKey { get; set; }

    /// <summary>
    /// Секретный ключ для подключения к облачному хранилищу.
    /// </summary>
    public required string SecretKey { get; set; }
}