using System.Security.Claims;
using Kafo.Application.Interfaces;
using Kafo.Server.Application.Authorization.Services.Models;

namespace Kafo.Server.Application.Authorization.Services;

public class UserInfo : IUserInfo
{
    public Guid? UserId { get; set; }
    public string? Username { get; set; }
    public Guid? Token { get; set; }
    public bool IsAuthenticated { get; set; }

    public static List<Claim> GetClaims(JwtData data)
    {
        var claims = new List<Claim>
        {
            new Claim("UserId", data.UserId.ToString()),
            new Claim("AuthorizationToken", data.UserToken.ToString()),
            new Claim(ClaimsIdentity.DefaultNameClaimType, data.Username)
        };

        return claims;
    }
}