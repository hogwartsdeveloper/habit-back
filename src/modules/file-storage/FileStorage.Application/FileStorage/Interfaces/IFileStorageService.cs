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
    /// Выполняет загрузку файла в указанное хранилище.
    /// </summary>
    /// <param name="bucketName">Имя хранилища, куда будет загружен файл.</param>
    /// <param name="fileName">Имя файла, под которым он будет сохранён в хранилище.</param>
    /// <param name="contentType">MIME-тип содержимого файла,
    /// который помогает в его идентификации и выборе программ для его открытия.</param>
    /// <param name="length">Размер файла в байтах,
    /// который необходим для оптимизации процесса загрузки и обработки данных.</param>
    /// <param name="stream">Поток данных файла, который содержит сам файл, предназначенный для загрузки.</param>
    /// <param name="cancellationToken">Токен отмены,
    /// позволяющий отменить операцию загрузки файла в случае необходимости.</param>
    /// <returns>Возвращает задачу, которая представляет асинхронную операцию загрузки файла,
    /// результат выполнения которой можно обрабатывать далее.</returns>
    Task UploadAsync(
        string bucketName,
        string fileName,
        string contentType,
        long length,
        Stream stream,
        CancellationToken cancellationToken = default);
    
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