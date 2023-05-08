namespace Kafo.Server.API.Middlewares;
public static class MiddlewareExtensions
{
    public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalExceptionHandler>();
    }

    public static void UseUserInfoMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<UserAuthorizationMiddleware>();
    }
}
