using Microsoft.AspNetCore.Http;

namespace Habit.Application.FileStorage.Models;

public class FileModel
{
    public required IFormFile File { get; set; }
}