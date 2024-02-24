using System.Net;
using AutoMapper;
using Habit.Application.FileStorage;
using Habit.Application.Repositories;
using Habit.Application.Users.Interfaces;
using Habit.Application.Users.Models;
using Habit.Domain.Entities;
using Habit.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Habit.Application.Users.Services;

public class UserService(
    IRepository<User> userRepository,
    IFileStorageService fileStorageService,
    IMapper mapper) : IUserService
{
    private string BucketName => "user";
    
    public async Task AddImageAsync(Guid id, IFormFile file, CancellationToken cancellationToken = default)
    {
        await fileStorageService.UploadAsync(BucketName, file, cancellationToken);
    }

    public async Task UpdateAsync(Guid id, UpdateUserModel model, CancellationToken cancellationToken = default)
    {
        var user = await userRepository
            .GetByIdAsync(id)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            throw new HttpException(HttpStatusCode.NotFound, "User not found");
        }

        mapper.Map(model, user);
        
        user.ChangeBirthDay(model.BirthDay);
        await userRepository.UpdateAsync(user, cancellationToken);
    }
}