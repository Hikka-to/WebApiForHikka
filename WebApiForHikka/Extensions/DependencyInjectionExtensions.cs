using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Constants.AppSettings;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.SharedFunction.HashFunction;
using WebApiForHikka.SharedFunction.JwtTokenFactories;
using WebApiForHikka.WebApi.Helper;
using WebApiForHikka.WebApi.Helper.FileHelper;

namespace WebApiForHikka.WebApi.Extensions;
public static class DependencyInjectionExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(AppSettingsStringConstants.DefaultConnection);
        services.AddDbContext<HikkaDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfiles()));
        var mapper = mapperConfiguration.CreateMapper();

        //Repositories
        var repositoryType = typeof(CrudRepository<>);
        var repositoryAssembly = repositoryType.Assembly;
        foreach (var repository in repositoryAssembly.GetTypes().Where(t =>
            (t.BaseType?.IsGenericType ?? false)
            && (t.BaseType.GetGenericTypeDefinition() == repositoryType)))
        {
            var repositoryInterface = repository.GetInterfaces().First(i => i.GetInterfaces().Any(si =>
                si.IsGenericType
                && (si.GetGenericTypeDefinition() == typeof(ICrudRepository<>))));
            services.AddScoped(repositoryInterface, repository);
        }

        //Services
        var serviceType = typeof(CrudService<,>);
        var serviceAssembly = serviceType.Assembly;
        foreach (var service in serviceAssembly.GetTypes().Where(t =>
            (t.BaseType?.IsGenericType ?? false)
            && (t.BaseType.GetGenericTypeDefinition() == serviceType)))
        {
            var serviceInterface = service.GetInterfaces().First(i => i.GetInterfaces().Any(si =>
                si.IsGenericType
                && (si.GetGenericTypeDefinition() == typeof(ICrudService<>))));
            services.AddScoped(serviceInterface, service);
        }

        //Helpers
        services.AddScoped<IHashFunctions, HashFunctions>();
        services.AddScoped<IFileHelper, FileHelper>();
        services.AddScoped<IJwtTokenFactory, JwtTokenFactory>();

    }
}