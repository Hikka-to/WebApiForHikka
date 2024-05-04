using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Constants.AppSettings;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Dtos.Helper;
using WebApiForHikka.SharedFunction.HashFunction;

namespace WebApiForHikka.Dtos.Extensions;
public static class DependencyInjectionExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(AppSettingsStringConstants.DefaultConnection);
        services.AddDbContext<HikkaDbContext>(options =>
            options.UseNpgsql(connectionString, x => x.MigrationsAssembly("WebApiForHikka.EfPersistence")));

        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfiles()));
        var mapper = mapperConfiguration.CreateMapper();


        //Repositories
        services.AddTransient<IUserRepository, UserRepository>();

        //Services
        services.AddTransient<IUserService, UserService>();

        //Helpers
        //!!!!! IMPORTANT if you change the hash function you need also change the verify password function in the UserPepository !!!!
        //!!!!! IMPORTANT if you change the hash function you need also change the hash function in test 
        services.AddSingleton<IHashFunctions, HashFunctions>(); 

    }

}
