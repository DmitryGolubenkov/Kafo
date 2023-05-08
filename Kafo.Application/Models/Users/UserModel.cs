namespace Kafo.Application.Models.Users;

public class UserModel
{
    public required Guid Id { get; set; }
    public required string Username { get; set; }
    public required string PhoneNumber { get; set; }
}