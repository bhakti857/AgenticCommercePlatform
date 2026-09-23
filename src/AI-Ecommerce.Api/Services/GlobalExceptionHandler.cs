using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace AI_Ecommerce.Api.Services
{
    /// <summary>
    /// Global unhandled-exception handler. Converts any exception that reaches the
    /// middleware pipeline into an RFC 7807 ProblemDetails response (HTTP 500)
    /// instead of ASP.NET Core's default empty 500 response. The exception message
    /// is only surfaced in the Development environment.
    /// </summary>
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment env) : IExceptionHandler
    {
        /// <inheritdoc />
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, "Unhandled exception for {Method} {Path}",
                httpContext.Request.Method, httpContext.Request.Path);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An unexpected error occurred.",
                Instance = httpContext.Request.Path,
                Type = "https://tools.ietf.org/html/rfc7807"
            };
            problemDetails.Extensions["traceId"] = httpContext.TraceIdentifier;
            if (env.IsDevelopment())
                problemDetails.Detail = exception.ToString();

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}