using System.Net;
using BuildingBlocks.Endpoints.Abstraction;
using BuildingBlocks.Errors.Exceptions;
using FileStorage.Application.FileStorage.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileStorage.Endpoints.Controllers;

/// <summary>
/// Контроллер для работы с файлами.
/// </summary>
[Authorize]
public class FileController(IFileStorageService service) : BaseController
{
    /// <summary>
    /// Получает файл по указанному пути.
    /// </summary>
    /// <param name="filePath">Путь к файлу.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель файла.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(FileStreamResult), 200)]
    public async Task<FileStreamResult> GetAsync([FromQuery] string filePath, CancellationToken cancellationToken)
    {
        var fileData = filePath.Split("/");
        var bucketName = fileData[0];
        var fileName = fileData[1];
        
        var stream = await service.GetAsync(bucketName, fileName, cancellationToken);
        if (stream is null)
        {
            throw new HttpException(HttpStatusCode.NotFound, "File not found");
        }
        
        return File(stream, "application/octet-stream", fileName);
    }
}