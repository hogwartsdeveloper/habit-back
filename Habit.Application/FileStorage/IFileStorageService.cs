using Microsoft.AspNetCore.Http;

namespace Habit.Application.FileStorage;

public interface IFileStorageService
{
    Task UploadAsync(string bucketName, IFormFile file, CancellationToken cancellationToken = default);

    Task RemoveAsync(string bucketName, string fileName, CancellationToken cancellationToken = default);
}