using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DocSync.API.Middlewares
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

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            string message = "An unexpected error occurred.";

            switch (ex)
            {
                case ArgumentNullException _:
                    statusCode = HttpStatusCode.BadRequest;
                    message = "A required parameter was null or empty or zero";
                    break;
                case FileNotFoundException _:
                    statusCode = HttpStatusCode.NotFound;
                    message = "The requested file was not found.";
                    break;
                case UnauthorizedAccessException _:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = "Access denied.";
                    break;
                case InvalidOperationException _:
                    statusCode = HttpStatusCode.BadRequest;
                    message = "Invalid operation.";
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var problemDetails = new ProblemDetails
            {
                Status = (int)statusCode,
                Title = message,
                Detail = ex.Message
            };
            return context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

}
