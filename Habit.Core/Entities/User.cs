using Habit.Core.Entities.Abstraction;

namespace Habit.Core.Entities;

public class User : BaseEntity
{
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public DateTime? BirthDay { get; set; }
    public required string PasswordHash { get; set; }
    public List<Habit>? Habits { get; set; }
}