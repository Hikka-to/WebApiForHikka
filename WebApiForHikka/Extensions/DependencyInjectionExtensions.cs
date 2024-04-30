using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Constants.AppSettings;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.WebApi.Helper.HashFunction;

namespace WebApiForHikka.WebApi.Extensions;
public static class DependencyInjectionExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(AppSettingsStringConstants.DefaultConnection);
        services.AddDbContext<HikkaDbContext>(options =>
            options.UseNpgsql(connectionString, x => x.MigrationsAssembly("WebApiForHikka.EfPersistence")));

        //Repositories
        services.AddTransient<IUserRepository, UserRepository>();

        //Services
        services.AddTransient<IUserService, UserService>();

        //Helpers
        services.AddSingleton<IHashFunctions, HashFunctions>();

    }

}
