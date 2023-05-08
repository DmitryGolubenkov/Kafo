using System.Security;

namespace Kafo.Desktop.UI.Models;
public class NewUserModel
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Пароль
    /// </summary>
    public SecureString? Password { get; set; }

    /// <summary>
    /// Пароль
    /// </summary>
    public SecureString? PasswordRepeat { get; set; }


    /// <summary>
    /// Номер телефона
    /// </summary>
    public string? PhoneNumber { get; set; }

}
