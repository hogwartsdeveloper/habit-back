using Microsoft.AspNetCore.Http;

namespace Habit.Application.FileStorage.Models;

/// <summary>
/// Модель файла.
/// </summary>
public class FileModel
{
    /// <summary>
    /// Файл.
    /// </summary>
    public required IFormFile File { get; set; }
}