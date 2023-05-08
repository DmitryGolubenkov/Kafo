namespace Kafo.Application.Interfaces;

public interface IUserInfo
{
    Guid? UserId { get; }
    bool IsAuthenticated { get; }
    Guid? Token { get; set; }
    string Username { get; set; }
}
