using System.Net;
using System.Text;
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

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        string errorMessage = await ex.ToErrorStrAsync() ?? "";
        _logger?.LogError($"{nameof(Exception)} details: {errorMessage}");
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(JsonConvert.SerializeObject(new
        {
            StatusCode = HttpStatusCode.InternalServerError,
            Message = errorMessage
        })).ConfigureAwait(false);
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        Stream originalBodyStream = context.Response.Body;
        try
        {
            string requestStr = await FormatRequest(context.Request);
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;
            await next(context);
            _logger?.Log(LogLevel.Information,
                $"Requested - {requestStr} {Environment.NewLine} Response - {await FormatResponse(context.Response)}");
            await responseBody.CopyToAsync(originalBodyStream);
        }
        catch (Exception exceptionObj)
        {
            context.Response.Body = originalBodyStream;
            await HandleExceptionAsync(context, exceptionObj).ConfigureAwait(false);
        }
    }

    private async Task<string> FormatResponse(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        string text = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);
        return $"{response.StatusCode}: {text}";
    }


    private async Task<string> FormatRequest(HttpRequest request)
    {
        var body = request.Body;
        request.EnableBuffering();
        var buffer = new byte[Convert.ToInt32(request.ContentLength)];
        int _ = await request.Body.ReadAsync(buffer, 0, buffer.Length);
        var bodyAsText = Encoding.UTF8.GetString(buffer);
        request.Body = body;
        return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
    }
}