using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.Relation.CountryAnimes;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Application.WithSeoAddition.Countries;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition.Animes;

namespace WebApiForHikka.Test.Controllers.RelationControllers;

public class CountryAnimeControllerTest : RelationCrudControllerTest<
    CountryAnime, Country, Anime,
    ICountryAnimeRelationService, ICountryService, IAnimeService,
    ICountryAnimeRelationRepository, ICountryRepository, IAnimeRepository,
    CountryAnimeController
>
{
    protected override async Task<CountryAnimeController> GetController(
        IServiceProvider alternativeServices)
    {
        return new CountryAnimeController(
            alternativeServices.GetRequiredService<ICountryAnimeRelationService>(),
            Mapper,
            await GetHttpContextAccessForAdminUser(
                alternativeServices.GetRequiredService<UserManager<User>>(),
                alternativeServices.GetRequiredService<RoleManager<IdentityRole<Guid>>>()
            )
        );
    }

    protected override Country GetFirstModelSample()
    {
        return GetCountryModels.GetSample();
    }

    protected override Anime GetSecondModelSample()
    {
        return GetAnimeModels.GetSample();
    }

    protected override void GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        alternativeServices.AddSingleton(dbContext);
        alternativeServices.AddSingleton(userManager);
        alternativeServices.AddSingleton(roleManager);


        alternativeServices.AddSingleton<IFileHelper, FileHelper>();

        alternativeServices.AddSingleton<IAnimeBackdropRepository, AnimeBackdropRepository>();
        alternativeServices.AddSingleton<IAnimeBackdropService, AnimeBackdropService>();


        alternativeServices.AddSingleton<IAnimeRepository, AnimeRepository>();
        alternativeServices.AddSingleton<IAnimeService, AnimeService>();

        alternativeServices.AddSingleton<ICountryRepository, CountryRepository>();
        alternativeServices.AddSingleton<ICountryService, CountryService>();

        alternativeServices
            .AddSingleton<ICountryAnimeRelationRepository, CountryAnimeRelationRepository>();
        alternativeServices
            .AddSingleton<ICountryAnimeRelationService, CountryAnimeRelationService>();
    }
}