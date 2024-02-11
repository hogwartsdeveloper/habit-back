using Habit.Application.Auth.Models;
using Habit.Domain.Entities;

namespace Habit.Application.Auth.Interfaces;

public interface ISecurityService
{
    string GenerateToken(User user);
    RefreshTokenModel GenerateRefreshToken();

    string HashPassword(string password);

    bool VerifyPassword(string password, string passwordHash);

    bool VerifyRefreshToken(RefreshTokenModel model);
}