using Habit.Core.Entities.Abstraction;

namespace Habit.Core.Entities;

public class User : EntityBase
{
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public DateTime? BirthDay { get; set; }
    public required string PasswordHash { get; set; }
    public string? RefreshToken { get; set; }
    public List<Habit>? Habits { get; set; }
    
    public List<UserVerify>? UserVerification { get; set; }
}