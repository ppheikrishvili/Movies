﻿using System.Buffers;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Movies.Application.Exceptions;
using Movies.Domain.Entity;
using Movies.Domain.Shared.Enums;
using Movies.Domain.Shared.Extensions;
using Newtonsoft.Json;

namespace Movies.Infrastructure.Middleware;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler>? logger) : IMiddleware
{
    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        string errorMessage = await ex.ToErrorStrAsync() ?? "";
        logger?.LogError("{Exception} details: {errorMessage}", nameof(Exception), errorMessage);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        await context.Response.WriteAsync(JsonConvert.SerializeObject(
            (ex is BaseException exBaseException)
                ? exBaseException.ResponseResult
                : new ResponseResult<Exception>
                {
                    ResponseCode = ResponseCodeEnum.InternalSystemError,
                    ResponseStr = errorMessage
                }, Formatting.None,
            new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
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
            logger?.Log(LogLevel.Information,
                "Requested - {requestStr} {NewLine} Response - {FormatResponse}", requestStr, Environment.NewLine, await FormatResponse(context.Response));
            await responseBody.CopyToAsync(originalBodyStream).ConfigureAwait(false);
        }
        catch (Exception exceptionObj)
        {
            context.Response.Body = originalBodyStream;
            await HandleExceptionAsync(context, exceptionObj).ConfigureAwait(false);
        }
    }

    private static async Task<string> FormatResponse(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        string text = await new StreamReader(response.Body).ReadToEndAsync().ConfigureAwait(false);
        response.Body.Seek(0, SeekOrigin.Begin);
        return $"{response.StatusCode}: {text}";
    }

    private static async Task<string> FormatRequest(HttpRequest request)
    {
        request.EnableBuffering();
        byte[] buffer = ArrayPool<byte>.Shared.Rent(Convert.ToInt32(request.ContentLength));
        int _ = await request.Body.ReadAsync(buffer.AsMemory(0, buffer.Length)).ConfigureAwait(false);
        var bodyAsText = Encoding.UTF8.GetString(buffer);
        ArrayPool<byte>.Shared.Return(buffer);
        request.Body.Seek(0, SeekOrigin.Begin);
        return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
    }
}