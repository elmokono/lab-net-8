using System.Runtime.CompilerServices;
using System.Text.Json;

namespace MyAwsApp
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate requestDelegate, ILogger<ErrorHandlingMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var result = JsonSerializer.Serialize(new
                {
                    error = "An unexpected error occurred.",
                    detail = ex.Message
                });

                await httpContext.Response.WriteAsync(result);
            }

        }
    }
}
