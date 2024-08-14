using Microsoft.AspNetCore.Diagnostics;
using WebApiForHikka.WebApi.Middlewares;

namespace WebApiForHikka.WebApi.Extensions;

static public class ExceptionHandlerMiddlewareExtensions
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
