using System.Diagnostics;

namespace hairDresser.Presentation.Middleware
{
    public class Middleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<Middleware> _logger;

        public Middleware(RequestDelegate next, ILogger<Middleware> logger)
        {
            _next = next;
            _logger = logger;

            _logger.LogInformation("Middleware started.");
            _logger.LogWarning("Something went wrong (testing).");
            _logger.LogError("Application error (testing).");
            _logger.LogCritical("Critical error (testing)");
            _logger.LogDebug("Debug (testing).");
            _logger.LogTrace("Trace (testing).");

            /* Order from low (Trace) to High (Critical):
             * Critical
             * Error
             * Warning
             * Information
             * Debug
             * Trace
             */
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation("Log in.");

            using var buffer = new MemoryStream();
            var request = httpContext.Request;
            var response = httpContext.Response;

            var stream = response.Body;
            response.Body = buffer;

            await _next.Invoke(httpContext);

            // ??? Multe din aceste informatii nu prea ajuta, sa mai caut si altele
            _logger.LogInformation($"Request content type: {httpContext.Request.Headers["Accept"]}" + $"{System.Environment.NewLine}" +
                $"Request path: {request.Path}" + $"{System.Environment.NewLine}" +
                $"Response type: {response.ContentType}" + $"{System.Environment.NewLine}" +
                $"Response length: {response.ContentLength ?? buffer.Length}");

            buffer.Position = 0;

            await buffer.CopyToAsync(stream);

            _logger.LogInformation("Log out.");
        }
    }
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware>();
        }
    }
}
