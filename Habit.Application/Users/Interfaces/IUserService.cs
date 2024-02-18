using Habit.Application.Users.Models;

namespace Habit.Application.Users.Interfaces;

public interface IUserService
{
    Task UpdateAsync(Guid id, UpdateUserModel model, CancellationToken cancellationToken = default);
}