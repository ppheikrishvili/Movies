using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Movies.Domain.Shared.Extensions;
using Newtonsoft.Json;

namespace Movies.Infrastructure.Middleware;

public class GlobalExceptionHandler : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandler>? _logger;
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler>? logger)
    {
        _logger = logger;
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger<GlobalExceptionHandler>? logger)
    {
        string errorMessage = await ex.ToErrorStr() ?? "";

        logger?.LogError($"Exception details: {errorMessage}" );

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //await context.Response.WriteAsync(result).ConfigureAwait(false);
        await context.Response.WriteAsync(JsonConvert.SerializeObject(new { StatusCode = HttpStatusCode.InternalServerError,
            Message = errorMessage })).ConfigureAwait(false);
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            //var F = await new StreamReader(context.Response.Body).ReadToEndAsync();
            _logger?.Log (LogLevel.Information,$"{await new StreamReader(context.Response.Body).ReadToEndAsync().ConfigureAwait(false)}");
            await next(context);
        }
        catch (Exception exceptionObj)
        {
            await HandleExceptionAsync(context, exceptionObj, _logger).ConfigureAwait(false);
        }
    }
}