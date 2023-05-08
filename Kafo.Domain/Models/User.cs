namespace Kafo.Domain.Models;

public class User : EntityBase
{
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public required string PasswordSalt { get; set; }
    public required Guid Token { get; set; }

    /// <summary>
    /// Номер телефона пользователя
    /// </summary>
    public string? PhoneNumber { get; set; }
}