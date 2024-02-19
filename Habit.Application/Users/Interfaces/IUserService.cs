using Habit.Application.Users.Models;
using Microsoft.AspNetCore.Http;

namespace Habit.Application.Users.Interfaces;

public interface IUserService
{
    Task AddImageAsync(Guid id, IFormFile file, CancellationToken cancellationToken = default);
    Task UpdateAsync(Guid id, UpdateUserModel model, CancellationToken cancellationToken = default);
}