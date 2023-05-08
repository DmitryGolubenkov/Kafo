using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Kafo.Server.Application.Authorization.Services;
using Kafo.Server.Application.Authorization.Services.Models;
using Microsoft.IdentityModel.Tokens;

namespace Kafo.Server.Application.Utilities;
internal static class JwtGenerator
{

    public static string CreateEncodedToken(JwtData data, AuthOptions options)
    {
        List<Claim> claims = UserInfo.GetClaims(data);

        var jwtToken = new JwtSecurityToken(
            issuer: options.Issuer,
            audience: options.Audience,
            notBefore: DateTime.UtcNow,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(30),
            signingCredentials: new SigningCredentials(options.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

        var encodedToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return encodedToken.ToString();
    }
}
