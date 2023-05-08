using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Kafo.Application.Interfaces;
using Kafo.Domain.Models;
using Kafo.Server.Application.Authorization.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Kafo.Server.API.Middlewares;

public class UserAuthorizationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<UserAuthorizationMiddleware> _logger;
    private readonly AuthOptions _authOptions;

    public UserAuthorizationMiddleware(RequestDelegate next, 
        ILogger<UserAuthorizationMiddleware> logger, 
        IOptions<AuthOptions> authOptions)
    {
        _logger = logger;
        _next = next;
        _authOptions = authOptions.Value;
    }


    public async Task InvokeAsync(HttpContext httpContext, UserInfo userInfo, IRepository<User> userRepository)
    {
        string? authHeader = httpContext.Request.Headers["Authorization"];
        if (authHeader != null)
        {
            authHeader = authHeader.Replace("Bearer ", "");
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                jwtTokenHandler.ValidateToken(authHeader, new TokenValidationParameters
                {
                    ValidIssuer = _authOptions.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = _authOptions.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = _authOptions.GetSymmetricSecurityKey()
                }, out _);
            }
            catch (SecurityTokenException)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await httpContext.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = "Unauthorized"
                }.ToString());
            }


            var claims = jwtTokenHandler.ReadJwtToken(authHeader).Claims.ToList();

            // Validate token using repository
            userRepository.Where(x => x.Token.ToString() == claims.First(x => x.Type == "AuthorizationToken").Value);

            var username = claims.First(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;

            userInfo.UserId = Guid.Parse(claims.First(claim => claim.Type == "UserId").Value);
            userInfo.Token = Guid.Parse(claims.First(x => x.Type == "AuthorizationToken").Value);
            userInfo.IsAuthenticated = true;
            userInfo.Username = username;
        }
        await _next(httpContext);
    }
}