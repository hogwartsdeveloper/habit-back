using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Shared.Models;

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