using AutoMapper;
using AutoMapper.QueryableExtensions;
using BuildingBlocks.Entity.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Users.Application.Users.Interfaces;
using Users.Application.Users.Models;
using Users.Domain.Users;

namespace Users.Application.Users.Services;

/// <inheritdoc />
public class UserService(
    IRepository<User> userRepo,
    IMapper mapper) : IUserService
{
    private string BucketName => "user";
    
    /// <inheritdoc />
    public async Task AddImageAsync(Guid id, IFormFile file, CancellationToken cancellationToken = default)
    {
        var user = await userRepo
            .GetByIdAsync(id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (user is null)
        {
            // Exception
        }

        if (user.ImageUrl != null)
        {
            var userImageData = user.ImageUrl.Split("/");
            // Integration Event
        }
        
        // Grpc
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
            // Exception
        }

        // Grpc FileModule
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
            // Exception
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
            // Exception
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
            // Exception
        }

        return user;
    }
}