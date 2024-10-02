using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.Relation.TagAnimes;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Application.WithSeoAddition.Animes;
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
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition.Animes;

namespace WebApiForHikka.Test.Controllers.RelationControllers;

public class TagAnimeControllerTest : RelationCrudControllerTest<
    TagAnime, Tag, Anime,
    ITagAnimeRelationService, ITagService, IAnimeService,
    ITagAnimeRelationRepository, ITagRepository, IAnimeRepository,
    TagAnimeController
>
{
    protected override async Task<TagAnimeController> GetController(
        IServiceProvider alternativeServices)
    {
        return new TagAnimeController(
            alternativeServices.GetRequiredService<ITagAnimeRelationService>(),
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

        alternativeServices.AddSingleton<ITagRepository, TagRepository>();
        alternativeServices.AddSingleton<ITagService, TagService>();

        alternativeServices.AddSingleton<ITagAnimeRelationRepository, TagAnimeRelationRepository>();
        alternativeServices.AddSingleton<ITagAnimeRelationService, TagAnimeRelationService>();
    }
}