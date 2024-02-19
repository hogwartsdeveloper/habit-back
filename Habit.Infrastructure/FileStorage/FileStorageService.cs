using Habit.Application.FileStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Minio;

namespace Infrastructure.FileStorage;

public class FileStorageService : IFileStorageService
{
    private readonly IMinioClient _client;
    
    public FileStorageService(IOptions<FileStorageSettings> options)
    {
        var settings = options.Value;
        
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