using Microsoft.AspNetCore.Diagnostics;
using TodoApi.Models;

namespace TodoApi.ErrorHandling;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {

        logger.LogError(
            exception,
            "Could not process a request on Machine{Machine}.",
            Environment.MachineName
        );

        if( exception is TodoStatusNotFoundException todoStatusNotFound)
        {
            await Results.NotFound(todoStatusNotFound.Message).ExecuteAsync(httpContext);
            return true;
        }
        if (exception is TodoNotFoundException todoNotFound)
        {
            await Results.NotFound(todoNotFound.Message).ExecuteAsync(httpContext);
            return true;
        }

            await Results.Problem(
            title: "An error occurred while processing your request.",
            statusCode: StatusCodes.Status500InternalServerError,
            detail: exception.Message
        ).ExecuteAsync(httpContext);

        return true;
    }
}
