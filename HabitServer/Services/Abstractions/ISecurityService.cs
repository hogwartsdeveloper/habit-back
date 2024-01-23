namespace HabitServer.Services.Abstractions;

public interface ISecurityService
{
    string GenerateToken();

    string HashPassword(string password);

    bool VerifyPassword(string password, string passwordHash);
}