namespace Habit.Application.Auth.Models;

public class ConfirmEmailModel
{
    public required string Email { get; set; }
    
    public required string Code { get; set; }
}