using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.Relation.DubAnimes;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Application.WithSeoAddition.Dubs;
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

public class DubAnimeControllerTest : RelationCrudControllerTest<
    DubAnime, Dub, Anime,
    IDubAnimeRelationService, IDubService, IAnimeService,
    IDubAnimeRelationRepository, IDubRepository, IAnimeRepository,
    DubAnimeController
>
{
    protected override async Task<DubAnimeController> GetController(
        IServiceProvider alternativeServices)
    {
        return new DubAnimeController(
            alternativeServices.GetRequiredService<IDubAnimeRelationService>(),
            Mapper,
            await GetHttpContextAccessForAdminUser(
                alternativeServices.GetRequiredService<UserManager<User>>(),
                alternativeServices.GetRequiredService<RoleManager<IdentityRole<Guid>>>()
            )
        );
    }

    protected override Dub GetFirstModelSample()
    {
        return GetDubModels.GetSample();
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

        alternativeServices.AddSingleton<IDubRepository, DubRepository>();
        alternativeServices.AddSingleton<IDubService, DubService>();

        alternativeServices.AddSingleton<IDubAnimeRelationRepository, DubAnimeRelationRepository>();
        alternativeServices.AddSingleton<IDubAnimeRelationService, DubAnimeRelationService>();
    }
}