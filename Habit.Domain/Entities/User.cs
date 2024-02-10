using Habit.Domain.Entities.Abstraction;

namespace Habit.Domain.Entities;

public class User : EntityBase
{
    private User()
    {
    }
    
    public User(
        string firstName,
        string lastName,
        string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public void Registration(string passwordHash, string refreshToken)
    {
        PasswordHash = passwordHash;
        UpdateRefreshToken(refreshToken);
        IsEmailConfirmed = false;
    }

    public void UpdateRefreshToken(string refreshToken)
    {
        RefreshToken = refreshToken;
    }

    public void ConfirmEmail()
    {
        IsEmailConfirmed = true;
    }

    public void ChangePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
    }
    
    public string FirstName { get; private set; }
    
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public bool IsEmailConfirmed { get; private set; }
    public DateTime? BirthDay { get; private set; }
    public string? PasswordHash { get; private set; }
    public string? RefreshToken { get; private set; }
    public List<Habit>? Habits { get; private set; }
    
    public List<UserVerify>? UserVerification { get; private set; }
}