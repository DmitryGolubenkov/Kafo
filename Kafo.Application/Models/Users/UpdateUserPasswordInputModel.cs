namespace Kafo.Application.Models.Users;

public class UpdateUserPasswordInputModel
{
    public Guid Id { get; set; }
    public string NewPassword { get; set; }
}
