namespace Kafo.Application.Models.Authorization;
public class UserLoginResultModel
{
    /// <summary>
    /// Успешна ли авторизация?
    /// </summary>
    public required bool IsSuccessful { get; set; }
    
    /// <summary>
    /// ID пользователя
    /// </summary>
    public Guid? UserId { get; set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// JWT токен
    /// </summary>
    public string? JwtToken { get; set; }

}
