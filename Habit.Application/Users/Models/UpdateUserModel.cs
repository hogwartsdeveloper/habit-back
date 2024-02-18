namespace Habit.Application.Users.Models;

public class UpdateUserModel
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    
    public DateTime? BirthDay { get; set; }
}