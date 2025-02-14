using System.Net;
using System.Text.Json;

namespace Factoring.API.Middleware
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

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = exception switch
            {
                ArgumentException => new { status = (int)HttpStatusCode.BadRequest, message = exception.Message },
                _ => new { status = (int)HttpStatusCode.InternalServerError, message = "An unexpected error occurred." }
            };

            context.Response.StatusCode = response.status;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
