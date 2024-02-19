using System.Net;
using Habit.Application.FileStorage;
using Habit.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

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

    public async Task UploadAsync(string bucketName, IFormFile file, CancellationToken cancellationToken)
    {
        try
        {
            await using var stream = file.OpenReadStream();
            var args = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithStreamData(stream)
                .WithFileName(file.FileName)
                .WithContentType(file.ContentType)
                .WithObjectSize(file.Length);

            await _client.PutObjectAsync(args, cancellationToken);
        }
        catch (Exception e)
        {
            throw new HttpException(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}