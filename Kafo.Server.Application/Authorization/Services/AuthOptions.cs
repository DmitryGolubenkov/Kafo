using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Kafo.Server.Application.Authorization.Services;

public class AuthOptions
{
    public string Issuer { get; set; } = "Kafo";
    public string Audience { get; set; } = "Kafo";
    public string SigningKey { get; set; } = "kafo_tatXs57UyeAJnfaSZDDKXCCJzULLJZw8q3CtPpzvV3XaCPs4KqsX6wuCpyUqRURt";
    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SigningKey));
    }

    public static SymmetricSecurityKey GetSymmetricSecurityKey(string key)
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
    }
}