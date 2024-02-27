using Habit.Api.Controllers.Abstractions;
using Habit.Application.FileStorage.Interfaces;
using Habit.Application.FileStorage.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Habit.Api.Controllers;

/// <summary>
/// Контроллер для работы с файлами.
/// </summary>
[Authorize]
public class FileController(IFileStorageService fileStorageService) : BaseController
{
    /// <summary>
    /// Получает файл по указанному пути.
    /// </summary>
    /// <param name="filePath">Путь к файлу.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель файла.</returns>
    [HttpGet]
    public Task<FileModel?> GetAsync([FromQuery] string filePath, CancellationToken cancellationToken)
    {
        var fileData = filePath.Split("/");
        var bucketName = fileData[0];
        var fileName = fileData[1];
        
        return fileStorageService.GetAsync(bucketName, fileName, cancellationToken);
    }
}