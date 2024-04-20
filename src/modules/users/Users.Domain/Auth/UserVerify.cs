using BuildingBlocks.Entity.Abstraction;
using Users.Domain.Users;

namespace Users.Domain.Auth;

/// <summary>
/// Класс, представляющий верификацию пользователя.
/// </summary>
public class UserVerify : EntityBase
{
    private UserVerify() {}
    
    /// <summary>
    /// Возвращает идентификатор пользователя, связанный с верификацией.
    /// </summary>
    public Guid UserId { get; private set; }
    
    /// <summary>
    /// Возвращает пользователя, связанного с верификацией.
    /// </summary>
    public User? User { get; private set; }
    
    /// <summary>
    /// Возвращает код верификации.
    /// </summary>
    public string Code { get; private set; }
    
    /// <summary>
    /// Возвращает дату истечения срока действия кода верификации.
    /// </summary>
    public DateTime Exp { get; private set; }
}