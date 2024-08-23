namespace WebApiForHikka.WebApi.Extensions;

public static class CorsExtensions
{
    public const string AllowAllOrigins = "AllowAllOrigins";
    public const string AllowSpecificOrigins = "AllowSpecificOrigins";

    public static IServiceCollection AddCorsPolicies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(AllowAllOrigins,
                builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        services.AddCors(options =>
        {
            options.AddPolicy(AllowSpecificOrigins,
                builder =>
                {
                    builder
                        .WithOrigins(configuration.GetValue<string[]>("Cors:AllowedOrigins") ?? [])
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        return services;
    }
}