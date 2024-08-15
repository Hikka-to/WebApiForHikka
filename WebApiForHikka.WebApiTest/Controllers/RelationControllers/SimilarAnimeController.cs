using WebApiForHikka.Application.Relation.Similars;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Test.Controllers.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition.Animes;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;

namespace WebApiForHikka.Test.Controllers.RelationControllers;

public class SimilarControllerTest : RelationCrudControllerTest<
    Similar, Anime, Anime,
    ISimilarRelationService, IAnimeService, IAnimeService,
    ISimilarRelationRepository, IAnimeRepository, IAnimeRepository,
    SimilarController
    >
{
    protected override async Task<SimilarController> GetController(
      IServiceProvider alternativeServices)
    {

        return new SimilarController(
            alternativeServices.GetRequiredService<ISimilarRelationService>(),
            Mapper,
            await GetHttpContextAccessForAdminUser(
                alternativeServices.GetRequiredService<UserManager<User>>(),
                alternativeServices.GetRequiredService<RoleManager<IdentityRole<Guid>>>()
                )
            );
    }

    protected override Anime GetFirstModelSample() 
    {
        return GetAnimeModels.GetSample(); 
    }
    
    protected override Anime GetSecondModelSample() 
    {

        return GetAnimeModels.GetSample(); 
    }

    protected override void GetAllServices(IServiceCollection alternativeServices) 
    {

        var dbContext = GetDatabaseContext();

        var repository = new SimilarRelationRepository(dbContext);
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

        alternativeServices.AddSingleton<ISimilarRelationRepository, SimilarRelationRepository>();
        alternativeServices.AddSingleton<ISimilarRelationService, SimilarRelationService>();

    }

}