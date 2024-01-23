namespace HabitServer.Entities;

public class User
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public DateTime? BirthDay { get; set; }
    public string PasswordHash { get; set; }
}