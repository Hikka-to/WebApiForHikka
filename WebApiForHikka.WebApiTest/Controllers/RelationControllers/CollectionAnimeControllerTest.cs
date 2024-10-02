using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.Relation.CollectionAnimes;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Application.WithSeoAddition.Collections;
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

public class CollectionAnimeControllerTest : RelationCrudControllerTest<
    CollectionAnime, Collection, Anime,
    ICollectionAnimeRelationService, ICollectionService, IAnimeService,
    ICollectionAnimeRelationRepository, ICollectionRepository, IAnimeRepository,
    CollectionAnimeController
>
{
    protected override async Task<CollectionAnimeController> GetController(
        IServiceProvider alternativeServices)
    {
        return new CollectionAnimeController(
            alternativeServices.GetRequiredService<ICollectionAnimeRelationService>(),
            Mapper,
            await GetHttpContextAccessForAdminUser(
                alternativeServices.GetRequiredService<UserManager<User>>(),
                alternativeServices.GetRequiredService<RoleManager<IdentityRole<Guid>>>()
            )
        );
    }

    protected override Collection GetFirstModelSample()
    {
        return GetCollectionModels.GetSample();
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


        alternativeServices.AddSingleton<IAnimeBackdropRepository, AnimeBackdropRepository>();
        alternativeServices.AddSingleton<IAnimeBackdropService, AnimeBackdropService>();

        alternativeServices.AddSingleton<IFileHelper, FileHelper>();

        alternativeServices.AddSingleton<IAnimeRepository, AnimeRepository>();
        alternativeServices.AddSingleton<IAnimeService, AnimeService>();

        alternativeServices.AddSingleton<ICollectionRepository, CollectionRepository>();
        alternativeServices.AddSingleton<ICollectionService, CollectionService>();

        alternativeServices
            .AddSingleton<ICollectionAnimeRelationRepository, CollectionAnimeRelationRepository>();
        alternativeServices
            .AddSingleton<ICollectionAnimeRelationService, CollectionAnimeRelationService>();
    }
}