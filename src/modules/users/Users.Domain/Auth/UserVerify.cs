using BuildingBlocks.Entity.Abstraction;
using Users.Domain.Auth.Enums;
using Users.Domain.Users;

namespace Users.Domain.Auth;

/// <summary>
/// Класс, представляющий верификацию пользователя.
/// </summary>
public class UserVerify : EntityBase
{
    private UserVerify() {}
    
    /// <summary>
    /// Инициализирует новый экземпляр класса UserVerify с указанным идентификатором пользователя,
    /// кодом верификации, датой истечения срока действия и типом верификации.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="code">Код верификации.</param>
    /// <param name="exp">Дата истечения срока действия.</param>
    /// <param name="userVerifyType">Тип верификации пользователя.</param>
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
    
    /// <summary>
    /// Возвращает тип верификации пользователя.
    /// </summary>
    public UserVerifyType VerifyType { get; private set; }
}