using WebApiForHikka.WebApi.Middlewares;

namespace WebApiForHikka.WebApi.Extensions;

public static class ExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }

    public static IServiceCollection AddExceptionHandlerMiddleware(this IServiceCollection services)
    {
        return services.AddTransient<GlobalExceptionHandlerMiddleware>();
    }
}