using Habit.Domain.Entities;

namespace Habit.Application.Auth.Models;

public class TokenViewModel
{
    public required string AccessToken { get; set; }
    public required RefreshToken RefreshToken { get; set; }
}

public record RefreshTokenModel
{
    public required string Token { get; set; }
    public required DateTime Expires { get; set; }
}