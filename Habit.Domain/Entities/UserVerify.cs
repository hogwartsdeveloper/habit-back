using Habit.Core.Enums;
using Habit.Domain.Entities.Abstraction;

namespace Habit.Domain.Entities;

public class UserVerify : EntityBase
{
    private UserVerify() {}

    public UserVerify(
        Guid userId,
        string code,
        DateTime exp,
        UserVerifyType userVerifyType)
    {
        UserId = userId;
        Code = code;
        Exp = exp;
        VerifyType = userVerifyType;
    }
    
    public Guid UserId { get; private set; }
    
    public User? User { get; private set; }
    
    public string Code { get; private set; }
    
    public DateTime Exp { get; private set; }
    
    public UserVerifyType VerifyType { get; private set; }
}