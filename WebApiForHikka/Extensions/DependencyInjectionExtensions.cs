using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.Shared;
using WebApiForHikka.Constants.AppSettings;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.SharedFunction.Extensions;
using WebApiForHikka.SharedFunction.HashFunction;
using WebApiForHikka.SharedFunction.JwtTokenFactories;
using WebApiForHikka.WebApi.Helper;
using WebApiForHikka.WebApi.Helper.FileHelper;

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
        services.AddDbContext<HikkaDbContext>(options => { options.UseNpgsql(connectionString); });

        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfiles()));
        var mapper = mapperConfiguration.CreateMapper();

        //Repositories
        services.AddBaseClassArchitecture(typeof(CrudRepository<>), typeof(ICrudRepository<>));

        //Services
        services.AddBaseClassArchitecture(typeof(CrudService<,>), typeof(ICrudService<>));

        //Helpers
        services.AddScoped<IHashFunctions, HashFunctions>();
        services.AddScoped<IFileHelper, FileHelper>();
        services.AddScoped<IJwtTokenFactory, JwtTokenFactory>();
    }
}