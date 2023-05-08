namespace Kafo.Server.Application.Authorization.Services.Models;

public class JwtData
{
    public required Guid UserId { get; set; }
    public required string Username { get; set; }
    public required Guid UserToken { get; set; }
}