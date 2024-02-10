using Habit.Core.Enums;
using Habit.Domain.Entities.Abstraction;

namespace Habit.Domain.Entities;

public class UserVerify : EntityBase
{
    public Guid UserId { get; set; }
    
    public User? User { get; set; }
    
    public required string Code { get; set; }
    
    public required DateTime Exp { get; set; }
    
    public required UserVerifyType VerifyType { get; set; }
}