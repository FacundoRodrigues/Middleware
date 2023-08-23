namespace API.Middleware
{
    public class ConventionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ConventionMiddleware> _logger;

        public ConventionMiddleware(RequestDelegate next, ILogger<ConventionMiddleware> logger)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }

    public static class ConventionMiddlewareExtensions
    {
        public static IApplicationBuilder UseConventionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ConventionMiddleware>();
        }
    }
}