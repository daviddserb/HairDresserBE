using hairDresser.Application.CustomExceptions;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Net;

namespace hairDresser.Presentation.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                _logger.LogInformation("Invoke in.");
                _logger.LogInformation($"Request path: {httpContext.Request.Path}\n");

                await _next.Invoke(httpContext);

                _logger.LogInformation("Invoke out.");
            }
            catch (NotFoundException exception)
            {
                _logger.LogError($"NotFoundException: {exception}");
                await HandleNotFoundException(httpContext, exception);
            }
            catch (ClientException exception)
            {
                _logger.LogError($"ClientException: {exception}");
                await HandleClientException(httpContext, exception);
            }
        }

        private Task HandleNotFoundException (HttpContext httpContext, NotFoundException exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }

        private Task HandleClientException (HttpContext httpContext, ClientException exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
