using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BuildingBlocks.Entity.Interfaces;
using BuildingBlocks.Errors.Exceptions;
using BuildingBlocks.IntegrationEvents.Events;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Users.Application.Constants;
using Users.Application.Users.Interfaces;
using Users.Application.Users.Models;
using Users.Domain.Users;

namespace Users.Infrastructure.Users.Services;

/// <inheritdoc />
public class UserService(
    IRepository<User> userRepo,
    IMapper mapper,
    IPublishEndpoint publishEndpoint,
    FileStorageGrpcService.FileStorageGrpcServiceClient fileStorageClient) : IUserService
{
    private static string BucketName => "user";
    
    /// <inheritdoc />
    public async Task AddImageAsync(Guid id, IFormFile file, CancellationToken cancellationToken = default)
    {
        var user = await userRepo
            .GetByIdAsync(id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (user is null)
        {
            throw new HttpException(HttpStatusCode.NotFound, UserConstants.UserNotFound);
        }

        if (user.ImageUrl != null)
        {
            var userImageData = user.ImageUrl.Split("/");
            await publishEndpoint.Publish<IRemoveFileEvent>(new
            {
                BucketName = userImageData[0],
                FileName = userImageData[1]
            }, cancellationToken);
        }

        await UploadImage(file, cancellationToken);
        user.ChangeImage($"{BucketName}/{file.FileName}");
        await userRepo.UpdateAsync(user, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteImageAsync(Guid id, string fileName, CancellationToken cancellationToken = default)
    {
        var user = await userRepo
            .GetByIdAsync(id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (user is null)
        {
            throw new HttpException(HttpStatusCode.NotFound, UserConstants.UserNotFound);
        }
        
        await publishEndpoint.Publish<IRemoveFileEvent>(new
        {
            BucketName,
            FileName = fileName
        }, cancellationToken);
        
        user.DeleteImage();
        await userRepo.UpdateAsync(user, cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Guid id, UpdateUserModel model, CancellationToken cancellationToken = default)
    {
        var user = await userRepo
            .GetByIdAsync(id)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            throw new HttpException(HttpStatusCode.NotFound, UserConstants.UserNotFound);
        }

        mapper.Map(model, user);
        
        user.ChangeBirthDay(model.BirthDay);
        await userRepo.UpdateAsync(user, cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateEmailAsync(Guid id, UpdateEmailModel model, CancellationToken cancellationToken = default)
    {
        var user = await userRepo
            .GetByIdAsync(id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (user is null)
        {
            throw new HttpException(HttpStatusCode.NotFound, UserConstants.UserNotFound);
        }
        
        user.ChangeEmail(model.Email);
        await userRepo.UpdateAsync(user, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ViewUserModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await userRepo
            .GetByIdAsync(id)
            .ProjectTo<ViewUserModel>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (user is null)
        {
            throw new HttpException(HttpStatusCode.NotFound, UserConstants.UserNotFound);
        }

        return user;
    }

    private async Task UploadImage(IFormFile file, CancellationToken cancellationToken)
    {
        const int bufferSize = 64 * 1024;
        var buffer = new byte[bufferSize];

        using var call = fileStorageClient.Upload(cancellationToken: cancellationToken);
        var metadata = new FileUploadRequest
        {
            BucketName = BucketName,
            FileName = file.FileName,
            ContentType = file.ContentType,
            Length = file.Length
        };
        
        await call.RequestStream.WriteAsync(metadata, cancellationToken);

        await using var stream = file.OpenReadStream();
        int bytesRead;
        while ((bytesRead = await stream.ReadAsync(buffer, cancellationToken)) > 0)
        {
            var request = new FileUploadRequest
            {
                Content = Google.Protobuf.ByteString.CopyFrom(buffer, 0, bytesRead)
            };

            await call.RequestStream.WriteAsync(request, cancellationToken);
        }
        await call.RequestStream.CompleteAsync();
    }
}