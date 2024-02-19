using Habit.Application.FileStorage;
using Microsoft.AspNetCore.Http;
using Minio;

namespace Infrastructure.FileStorage;

public class FileStorageService : IFileStorageService
{
    private readonly IMinioClient _client;
    
    public FileStorageService(FileStorageSettings settings)
    {
        _client = new MinioClient()
            .WithEndpoint(settings.Endpoint)
            .WithCredentials(settings.AccessKey, settings.SecretKey)
            .WithSSL()
            .Build();
    }

    public Task UploadAsync(IFormFile file, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}