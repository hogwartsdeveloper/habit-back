namespace Habit.Application.Auth.Models;

public class RegistrationModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public DateTime? BirthDay { get; set; }
    public required string Password { get; set; }
}