using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.Relation.AnimeCharacters;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Application.WithSeoAddition.Characters;
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition.Characters;

namespace WebApiForHikka.Test.Controllers.RelationControllers;

public class AnimeCharacterControllerTest: RelationCrudControllerTest<
    AnimeCharacter, Anime, Character,
    IAnimeCharacterRelationService, IAnimeService, ICharacterService,
    IAnimeCharacterRelationRepository, IAnimeRepository, ICharacterRepository,
    AnimeCharacterController
    >
{
    protected override async Task<AnimeCharacterController> GetController(
      IServiceProvider alternativeServices)
    {

        return new AnimeCharacterController(
            alternativeServices.GetRequiredService<IAnimeCharacterRelationService>(),
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
    
    protected override Character GetSecondModelSample() 
    {

        return GetCharacterModels.GetSample(); 
    }

    protected override void GetAllServices(IServiceCollection alternativeServices) 
    {

        var dbContext = GetDatabaseContext();

        var repository = new AnimeCharacterRelationRepository(dbContext);
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

        alternativeServices.AddSingleton<ICharacterRepository, CharacterRepository>();
        alternativeServices.AddSingleton<ICharacterService, CharacterService>();

        alternativeServices.AddSingleton<IAnimeCharacterRelationRepository, AnimeCharacterRelationRepository>();
        alternativeServices.AddSingleton<IAnimeCharacterRelationService, AnimeCharacterRelationService>();
    }

}