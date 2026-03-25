using CustomerApi.Api.ExceptionHandling;
using CustomerApi.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CustomerApi.Api.Middleware
{
    public sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly ExceptionMappingOptions _mapping;

        public GlobalExceptionHandler(
            ILogger<GlobalExceptionHandler> logger,
            ExceptionMappingOptions mapping)
        {
            _logger = logger;
            _mapping = mapping;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Unhandled exception occurred.");

            _mapping.TryGetStatusCode(exception, out var statusCode);

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = "Error",
                Detail = statusCode == 500
                    ? "An unexpected error occurred."
                    : exception.Message,
                Instance = httpContext.Request.Path
            };

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
