using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MoviesApp.Middlewares;

public class RequestActorsLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public RequestActorsLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<RequestActorsLoggingMiddleware>();
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        finally
        {
            var reqPath = context.Request.Path.ToString();
            if (reqPath.Contains("Actors") || reqPath.Contains("actors"))
            {
                _logger.LogInformation(
                    "Request: {Method} {Url} => {StatusCode}; {Query}; {@Body};",
                    context.Request.Method,
                    context.Request.Path.Value,
                    context.Response.StatusCode,
                    context.Request.QueryString.Value,
                    await new StreamReader(context.Request.Body).ReadToEndAsync());
            }
        }
    }
}