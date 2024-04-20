using Microsoft.AspNetCore.Http;

namespace FileStorage.Application.FileStorage.Interfaces;

/// <summary>
/// Сервис файлового хранилища.
/// </summary>
public interface IFileStorageService
{
    /// <summary>
    /// Выполняет загрузку файла в указанное хранилище.
    /// </summary>
    /// <param name="bucketName">Имя хранилища.</param>
    /// <param name="file">Загружаемый файл.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию загрузки.</returns>
    Task UploadAsync(string bucketName, IFormFile file, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Удаляет файл из указанного хранилища.
    /// </summary>
    /// <param name="bucketName">Имя хранилища.</param>
    /// <param name="fileName">Имя удаляемого файла.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию удаления.</returns>
    Task RemoveAsync(string bucketName, string fileName, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Получает файл из указанного хранилища.
    /// </summary>
    /// <param name="bucketName">Имя хранилища.</param>
    /// <param name="fileName">Имя запрашиваемого файла.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Задача, представляющая асинхронную операцию получения файла.
    /// Возвращает null, если файл не найден.</returns>
    Task<Stream?> GetAsync(string bucketName, string fileName, CancellationToken cancellationToken = default);
}