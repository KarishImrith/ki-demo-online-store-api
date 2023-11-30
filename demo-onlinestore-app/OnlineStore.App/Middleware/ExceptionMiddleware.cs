using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;
using System.Security.Authentication;

namespace OnlineStore.App.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            var problemDetails = new ProblemDetails();

            switch (exception)
            {
                case AuthenticationException:
                    problemDetails.Title = HttpStatusCode.Unauthorized.ToString();
                    problemDetails.Detail = exception.Message;
                    problemDetails.Status = (int)HttpStatusCode.Unauthorized;
                    break;

                case KeyNotFoundException:
                    problemDetails.Title = HttpStatusCode.NotFound.ToString();
                    problemDetails.Detail = exception.Message;
                    problemDetails.Status = (int)HttpStatusCode.NotFound;
                    break;

                case UnauthorizedAccessException:
                    problemDetails.Title = HttpStatusCode.Forbidden.ToString();
                    problemDetails.Detail = exception.Message;
                    problemDetails.Status = (int)HttpStatusCode.Forbidden;
                    break;

                case ValidationException:
                    problemDetails.Title = HttpStatusCode.BadRequest.ToString();
                    problemDetails.Detail = exception.Message;
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    break;

                default:
                    problemDetails.Title = HttpStatusCode.InternalServerError.ToString();
                    problemDetails.Detail = exception.Message;
                    problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                    _logger.LogError(exception, "HTTP Request Error: {title}", problemDetails.Title);
                    break;
            }

            httpContext.Response.ContentType = MediaTypeNames.Application.Json;
            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
