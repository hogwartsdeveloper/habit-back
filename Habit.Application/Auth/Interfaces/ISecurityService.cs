using Habit.Application.Auth.Models;
using Habit.Domain.Entities;

namespace Habit.Application.Auth.Interfaces;

public interface ISecurityService
{
    TokenViewModel GenerateToken(User user);

    string HashPassword(string password);

    bool VerifyPassword(string password, string passwordHash);

    Task<bool> ValidateTokenAsync(string token);
}