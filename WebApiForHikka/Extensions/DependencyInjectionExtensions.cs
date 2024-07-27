using Microsoft.EntityFrameworkCore;
using Npgsql;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Constants.AppSettings;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.SharedFunction.Extensions;
using WebApiForHikka.SharedFunction.HashFunction;
using WebApiForHikka.SharedFunction.Helpers.ColorHelper;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;
using WebApiForHikka.SharedFunction.Helpers.LinkFactory;
using WebApiForHikka.SharedFunction.JwtTokenFactories;

namespace WebApiForHikka.WebApi.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddBaseClassArchitecture(this IServiceCollection services, Type baseClass, Type baseInterface)
    {
        foreach (var service in baseClass.Assembly.GetTypes().Where(t =>
                     !t.IsAbstract &&
                     !t.IsGenericTypeDefinition &&
                     t.GenericIsSubclassOf(baseClass)))
        {
            var interfaceType = service.GetInterfaces().Last(i => i.GetInterfaces().Any(si =>
                si.IsGenericType
                && si.GetGenericTypeDefinition() == baseInterface));
            services.AddScoped(interfaceType, service);
        }
    }

    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(AppSettingsStringConstants.DefaultConnection);
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

        //Repositories
        services.AddBaseClassArchitecture(typeof(CrudRepository<>), typeof(ICrudRepository<>));

        //Services
        services.AddBaseClassArchitecture(typeof(CrudService<,>), typeof(ICrudService<>));

        //Helpers
        services.AddScoped<IHashFunctions, HashFunctions>();
        services.AddScoped<IFileHelper, FileHelper>();
        services.AddScoped<IColorHelper, ColorHelper>();
        services.AddScoped<IJwtTokenFactory, JwtTokenFactory>();
        services.AddScoped<ILinkFactory, LinkFactory>();
    }
}