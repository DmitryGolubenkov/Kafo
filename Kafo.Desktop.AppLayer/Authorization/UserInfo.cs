namespace Kafo.Desktop.AppLayer.Authorization;

/// <summary>
/// Данные о пользователе, который авторизован в десктопном приложении
/// </summary>
public class UserInfo
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string? Username { get; private set; }
    
    /// <summary>
    /// Токен пользователя
    /// </summary>
    public string? JwtToken { get; private set; }
    
    /// <summary>
    /// Номер телефона
    /// </summary>
    public string? PhoneNumber { get; private set; }

    /// <summary>
    /// Вход выполнен?
    /// </summary>
    public bool IsLoggedIn { get; private set; }
    
    /// <summary>
    /// ID пользователя
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Начинает сессию пользователя
    /// </summary>
    public void StartSession(Guid userId, string username, string jwtToken, string phoneNumber)
    {
        IsLoggedIn = true;
        Username = username;
        JwtToken = jwtToken;
        UserId = userId;
        PhoneNumber = phoneNumber;
    }

    /// <summary>
    /// Завершает сессию пользователя
    /// </summary>
    public void FinishSession()
    {
        IsLoggedIn = false;
        Username = null;
        JwtToken = null;
        UserId = null;
    }
}