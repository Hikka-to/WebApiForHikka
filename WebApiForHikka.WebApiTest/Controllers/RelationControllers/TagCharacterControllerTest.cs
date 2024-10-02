using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.Relation.TagCharacters;
using WebApiForHikka.Application.WithSeoAddition.Characters;
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition.Characters;

namespace WebApiForHikka.Test.Controllers.RelationControllers;

public class TagCharacterControllerTest : RelationCrudControllerTest<
    TagCharacter, Tag, Character,
    ITagCharacterRelationService, ITagService, ICharacterService,
    ITagCharacterRelationRepository, ITagRepository, ICharacterRepository,
    TagCharacterController
>
{
    protected override async Task<TagCharacterController> GetController(
        IServiceProvider alternativeServices)
    {
        return new TagCharacterController(
            alternativeServices.GetRequiredService<ITagCharacterRelationService>(),
            Mapper,
            await GetHttpContextAccessForAdminUser(
                alternativeServices.GetRequiredService<UserManager<User>>(),
                alternativeServices.GetRequiredService<RoleManager<IdentityRole<Guid>>>()
            )
        );
    }

    protected override Tag GetFirstModelSample()
    {
        return GetTagModels.GetSample();
    }

    protected override Character GetSecondModelSample()
    {
        return GetCharacterModels.GetSample();
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

        alternativeServices.AddSingleton<ICharacterRepository, CharacterRepository>();
        alternativeServices.AddSingleton<ICharacterService, CharacterService>();

        alternativeServices.AddSingleton<ITagRepository, TagRepository>();
        alternativeServices.AddSingleton<ITagService, TagService>();

        alternativeServices
            .AddSingleton<ITagCharacterRelationRepository, TagCharacterRelationRepository>();
        alternativeServices
            .AddSingleton<ITagCharacterRelationService, TagCharacterRelationService>();
    }
}