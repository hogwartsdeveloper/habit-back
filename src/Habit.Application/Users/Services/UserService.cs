using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Habit.Application.Auth.Constants;
using Habit.Application.Errors;
using Habit.Application.FileStorage.Interfaces;
using Habit.Application.Repositories;
using Habit.Application.Users.Interfaces;
using Habit.Application.Users.Models;
using Habit.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Habit.Application.Users.Services;

/// <inheritdoc />
public class UserService(
    IRepository<User> userRepository,
    IFileStorageService fileStorageService,
    IMapper mapper) : IUserService
{
    private string BucketName => "user";
    
    /// <inheritdoc />
    public async Task AddImageAsync(Guid id, IFormFile file, CancellationToken cancellationToken = default)
    {
        var user = await userRepository
            .GetByIdAsync(id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (user is null)
        {
            throw new HttpException(HttpStatusCode.NotFound, AuthConstants.UserNotFound);
        }

        if (user.ImageUrl != null)
        {
            var userImageData = user.ImageUrl.Split("/");
            await fileStorageService.RemoveAsync(userImageData[0], userImageData[1], cancellationToken);
        }
        
        await fileStorageService.UploadAsync(BucketName, file, cancellationToken);
        user.ChangeImage($"{BucketName}/{file.FileName}");
        await userRepository.UpdateAsync(user, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteImageAsync(Guid id, string fileName, CancellationToken cancellationToken = default)
    {
        var user = await userRepository
            .GetByIdAsync(id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (user is null)
        {
            throw new HttpException(HttpStatusCode.NotFound, AuthConstants.UserNotFound);
        }

        await fileStorageService.RemoveAsync(BucketName, fileName, cancellationToken);
        user.DeleteImage();
        await userRepository.UpdateAsync(user, cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Guid id, UpdateUserModel model, CancellationToken cancellationToken = default)
    {
        var user = await userRepository
            .GetByIdAsync(id)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            throw new HttpException(HttpStatusCode.NotFound, AuthConstants.UserNotFound);
        }

        mapper.Map(model, user);
        
        user.ChangeBirthDay(model.BirthDay);
        await userRepository.UpdateAsync(user, cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateEmailAsync(Guid id, UpdateEmailModel model, CancellationToken cancellationToken = default)
    {
        var user = await userRepository
            .GetByIdAsync(id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (user is null)
        {
            throw new HttpException(HttpStatusCode.NotFound, AuthConstants.UserNotFound);
        }
        
        user.ChangeEmail(model.Email);
        await userRepository.UpdateAsync(user, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<UserViewModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await userRepository
            .GetByIdAsync(id)
            .ProjectTo<UserViewModel>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (user is null)
        {
            throw new HttpException(HttpStatusCode.NotFound, AuthConstants.UserNotFound);
        }

        return user;
    }
}