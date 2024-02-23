using System.Net;
using Habit.Application.FileStorage;
using Habit.Application.FileStorage.Models;
using Habit.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace Infrastructure.FileStorage;

/// <inheritdoc />
public class FileStorageService : IFileStorageService
{
    private readonly IMinioClient _client;
    
    public FileStorageService(IOptions<FileStorageSettings> options)
    {
        var settings = options.Value;
        
        _client = new MinioClient()
            .WithEndpoint(settings.Endpoint)
            .WithCredentials(settings.AccessKey, settings.SecretKey)
            .Build();
    }
    
    /// <inheritdoc />
    public async Task UploadAsync(string bucketName, IFormFile file, CancellationToken cancellationToken = default)
    {
        await CheckAndCreateBucketAsync(bucketName, cancellationToken);
        
        try
        {
            await using var stream = file.OpenReadStream();
            var args = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(file.FileName)
                .WithContentType(file.ContentType)
                .WithStreamData(stream)
                .WithObjectSize(file.Length);

            await _client.PutObjectAsync(args, cancellationToken);
        }
        catch (Exception e)
        {
            throw new HttpException(HttpStatusCode.InternalServerError, e.Message);
        }
    }
    
    /// <inheritdoc />
    public async Task RemoveAsync(string bucketName, string fileName, CancellationToken cancellationToken = default)
    {
        try
        {
            var args = new RemoveObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileName);

            await _client.RemoveObjectAsync(args, cancellationToken);
        }
        catch (Exception e)
        {
            throw new HttpException(HttpStatusCode.InternalServerError, e.Message);
        }
    }
    
    /// <inheritdoc />
    public async Task<FileModel?> GetAsync(string bucketName, string fileName, CancellationToken cancellationToken = default)
    {
        try
        {
            Stream? fileStream = null;
            var args = new GetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(fileName)
                .WithCallbackStream(stream =>
                {
                    fileStream = new MemoryStream();
                    stream.CopyTo(fileStream);
                    fileStream.Position = 0;
                });

            var stat = await _client.GetObjectAsync(args, cancellationToken);
            if (fileStream != null)
            {
                var file = new FormFile(
                    fileStream,
                    fileStream.Position,
                    stat.Size,
                    stat.ObjectName,
                    stat.ObjectName);

                return new FileModel { File = file };
            }

            return null;
        }
        catch (Exception e)
        {
            throw new HttpException(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    private async Task CheckAndCreateBucketAsync(string bucketName, CancellationToken cancellationToken)
    {
        var found = await CheckBucketExistsAsync(bucketName, cancellationToken);
        
        if (!found)
        {
            await CreateBucketAsync(bucketName, cancellationToken);
        }
    }

    private async Task<bool> CheckBucketExistsAsync(string bucketName, CancellationToken cancellationToken)
    {
        try
        {
            var args = new BucketExistsArgs().WithBucket(bucketName);
            return await _client.BucketExistsAsync(args, cancellationToken);
        }
        catch (Exception e)
        {
            throw new HttpException(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    private async Task CreateBucketAsync(string bucketName, CancellationToken cancellationToken)
    {
        try
        {
            var args = new MakeBucketArgs().WithBucket(bucketName);
            await _client.MakeBucketAsync(args, cancellationToken);
        }
        catch (Exception e)
        {
            throw new HttpException(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}