using Habit.Domain.Entities.Abstraction;

namespace Habit.Domain.Entities;

public class RefreshToken : EntityBase
{
    private RefreshToken() {}
    
    public RefreshToken(Guid userId, string token, DateTime expires)
    {
        UserId = userId;
        Token = token;
        Expires = expires;
    }

    public void UpdateToken(string token, DateTime expires)
    {
        Token = token;
        Expires = expires;
    }
    
    public Guid UserId { get; private set; }
    public User? User { get; private set; }
    public string Token { get; private set; }
    public DateTime Expires { get; private set; }
}