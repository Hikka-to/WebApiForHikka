using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiForHikka.Application.Formats;
using WebApiForHikka.Application.Kinds;
using WebApiForHikka.Application.Periods;
using WebApiForHikka.Application.RestrictedRatings;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.Sources;
using WebApiForHikka.Application.Statuses;
using WebApiForHikka.Application.Users;
using WebApiForHikka.Application.WithSeoAddition.Countries;
using WebApiForHikka.Application.WithSeoAddition.Dubs;
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Constants.AppSettings;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedFunction.HashFunction;
using WebApiForHikka.SharedFunction.JwtTokenFactories;
using WebApiForHikka.WebApi.Helper;

namespace WebApiForHikka.WebApi.Extensions;
public static class DependencyInjectionExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(AppSettingsStringConstants.DefaultConnection);
        services.AddDbContext<HikkaDbContext>(options =>
        {
            options.UseNpgsql(connectionString, x => x.MigrationsAssembly("WebApiForHikka.EfPersistence"));
        });

        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfiles()));
        var mapper = mapperConfiguration.CreateMapper();


        //Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISeoAdditionRepository, SeoAdditionRepository>();
        services.AddScoped<IStatusRepository, StatusRepository>();
        services.AddScoped<ISourceRepository, SourceRepository>();
        services.AddScoped<IRestrictedRatingRepository, RestrictedRatingRepository>();
        services.AddScoped<IPeriodRepository, PeriodRepository>();
        services.AddScoped<IKindRepository, KindRepository>();
        services.AddScoped<IFormatRepository, FormatRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IDubRepository, DubRepository>();

        //Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ISeoAdditionService, SeoAdditionService>();
        services.AddScoped<IStatusService, StatusService>();
        services.AddScoped<ISourceService, SourceService>();
        services.AddScoped<IRestrictedRatingService, RestrictedRatingService>();
        services.AddScoped<IPeriodService, PeriodService>();
        services.AddScoped<IKindService, KindService>();
        services.AddScoped<IFormatService, FormatService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<IDubService, DubService>();

        //Helpers
        services.AddScoped<IHashFunctions, HashFunctions>();
        services.AddScoped<IJwtTokenFactory, JwtTokenFactory>();

    }
}