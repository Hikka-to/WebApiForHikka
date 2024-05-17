using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Constants.AppSettings;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Dtos.Helper;
using WebApiForHikka.SharedFunction.HashFunction;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.Sources;
using WebApiForHikka.Application.Kinds;
using WebApiForHikka.Application.Periods;
using WebApiForHikka.Application.RestrictedRatings;
using WebApiForHikka.Application.Formats;
using WebApiForHikka.Application.Statuses;
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

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
        services.AddTransient<ISeoAdditionRepository, SeoAdditionRepository>();
        services.AddTransient<IStatusRepository, StatusRepository>();
        services.AddTransient<ISourceRepository, SourceRepository>();
        services.AddTransient<IRestrictedRatingRepository, RestrictedRatingRepository>();
        services.AddTransient<IPeriodRepository, PeriodRepository>();
        services.AddTransient<IKindRepository, KindRepository>();
        services.AddTransient<IFormatRepository, FormatRepository>();
        services.AddTransient<ITagRepository, TagRepository>();

        //Services
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ISeoAdditionService, SeoAdditionService>();
        services.AddTransient<IStatusService, StatusService>();
        services.AddTransient<ISourceService, SourceService>();
        services.AddTransient<IRestrictedRatingService, RestrictedRatingService>();
        services.AddTransient<IPeriodService, PeriodService>();
        services.AddTransient<IKindService, KindService>();
        services.AddTransient<IFormatService, FormatService>();
        services.AddTransient<ITagService, TagService>();

        //Helpers
        services.AddSingleton<IHashFunctions, HashFunctions>(); 

    }

}
