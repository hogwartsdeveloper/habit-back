using Habit.Core.Entities;

namespace Habit.Application.Auth.Interfaces;

public interface ISecurityService
{
    string GenerateToken(User user);

    string HashPassword(string password);

    bool VerifyPassword(string password, string passwordHash);
}