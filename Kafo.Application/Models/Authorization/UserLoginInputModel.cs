namespace Kafo.Application.Models.Authorization;
public class UserLoginInputModel
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public required string UserName { get; set; }

    /// <summary>
    /// Пароль пользователя 
    /// </summary>
    public required string Password { get; set; }
}
