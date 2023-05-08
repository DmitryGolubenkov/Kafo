namespace Kafo.Application.Models.Users;

public class CreateNewUserInputModel
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public required string Username { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public required string Password { get; set; }
    
    /// <summary>
    /// Номер телефона
    /// </summary>
    public required string PhoneNumber { get; set; }
}