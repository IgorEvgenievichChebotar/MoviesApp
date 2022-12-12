using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace MoviesApp.Middlewares;

public static class RequestActorsLoggingMiddlewareExtension
{
    public static IApplicationBuilder UseRequestLoggingToActors(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestActorsLoggingMiddleware>();
    }
}