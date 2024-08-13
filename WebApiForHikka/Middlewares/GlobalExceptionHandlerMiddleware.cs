using Microsoft.AspNetCore.Mvc;

namespace WebApiForHikka.WebApi.Middlewares;

public class GlobalExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
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
            var traceId = Guid.NewGuid();
            _logger.LogError($"Error occure while processing the request, TraceId : ${traceId}, Message : ${ex.Message}, StackTrace: ${ex.StackTrace}");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var problemDetails = new ProblemDetails
            {
                Title = "Internal Server Error",
                Status = (int)StatusCodes.Status500InternalServerError,
                Instance = context.Request.Path,
                Detail = $"Internal server error occured, traceId : {traceId}",
            };
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}