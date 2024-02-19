using Microsoft.AspNetCore.Http;

namespace Habit.Application.FileStorage;

public interface IFileStorageService
{
    Task UploadAsync(IFormFile file, CancellationToken cancellationToken);
}