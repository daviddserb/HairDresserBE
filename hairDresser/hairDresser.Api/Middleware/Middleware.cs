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
            _logger.LogInformation("Middleware is started.");
            _logger.LogWarning("Something went wrong (testing).");
            _logger.LogError("Application error (testing).");
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation("Log in.");
            await _next.Invoke(httpContext);
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
