namespace API.Middleware
{
    public class FactoryMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public FactoryMiddleware(ILogger logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}