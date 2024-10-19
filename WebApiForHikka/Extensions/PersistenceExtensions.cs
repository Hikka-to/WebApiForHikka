using Microsoft.EntityFrameworkCore;
using Npgsql;
using WebApiForHikka.Constants.AppSettings;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.WebApi.Extensions;

public static class PersistenceExtensions
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString(AppSettingsStringConstants.DefaultConnection);
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        var modelsAssembly = typeof(IModel).Assembly;
        var enumTypes = modelsAssembly.GetTypes().Where(t =>
            (t.Namespace?.Contains("Enums") ?? false) && t.IsEnum);
        foreach (var enumType in enumTypes)
            dataSourceBuilder.MapEnum(enumType);
        var dataSource = dataSourceBuilder.Build();
        services.AddDbContext<HikkaDbContext>(options =>
        {
            options
                .UseNpgsql(dataSource)
                .UseLazyLoadingProxies();
        });
    }

    public static void InitMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<HikkaDbContext>();
        context.Database.Migrate();
    }
}