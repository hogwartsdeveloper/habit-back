using System.Net;
using AutoMapper;
using Habit.Application.Repositories;
using Habit.Application.Users.Interfaces;
using Habit.Application.Users.Models;
using Habit.Core.Exceptions;
using Habit.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Habit.Application.Users.Services;

public class UserService(
    IRepository<User> userRepository,
    IMapper mapper) : IUserService
{
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