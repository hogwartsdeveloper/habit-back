using HabitServer.Entities;

namespace HabitServer.Services.Abstractions;

public interface ISecurityService
{
    string GenerateToken(User user);

    string HashPassword(string password);

    bool VerifyPassword(string password, string passwordHash);
}