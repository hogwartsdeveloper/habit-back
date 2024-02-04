namespace Habit.Application.Auth.Models;

public class TokenViewModel
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}