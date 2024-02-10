using Habit.Domain.Entities.Abstraction;

namespace Habit.Domain.Entities;

public class User : EntityBase
{
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public DateTime? BirthDay { get; set; }
    public required string PasswordHash { get; set; }
    public string? RefreshToken { get; set; }
    public List<Domain.Entities.Habit>? Habits { get; set; }
    
    public List<UserVerify>? UserVerification { get; set; }
}