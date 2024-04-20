using Users.Domain.Users;

namespace Users.Domain.Auth;

/// <summary>
/// Класс, представляющий токен обновления.
/// </summary>
public class RefreshToken
{
    private RefreshToken() {}
    
    /// <summary>
    /// Инициализирует новый экземпляр класса RefreshToken с указанным идентификатором пользователя,
    /// токеном и датой истечения срока действия.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="token">Токен обновления.</param>
    /// <param name="expires">Дата истечения срока действия токена.</param>
    public RefreshToken(Guid userId, string token, DateTime expires)
    {
        UserId = userId;
        Token = token;
        Expires = expires;
    }
    
    /// <summary>
    /// Обновляет токен и дату истечения срока действия.
    /// </summary>
    /// <param name="token">Новый токен.</param>
    /// <param name="expires">Новая дата истечения срока действия токена.</param>
    public void UpdateToken(string token, DateTime expires)
    {
        Token = token;
        Expires = expires;
    }
    
    /// <summary>
    /// Возвращает идентификатор пользователя, связанный с токеном.
    /// </summary>
    public Guid UserId { get; private set; }
    
    /// <summary>
    /// Возвращает пользователя, связанного с токеном.
    /// </summary>
    public User? User { get; private set; }
    
    /// <summary>
    /// Возвращает токен обновления.
    /// </summary>
    public string Token { get; private set; }
    
    /// <summary>
    /// Возвращает дату истечения срока действия токена.
    /// </summary>
    public DateTime Expires { get; private set; }
}