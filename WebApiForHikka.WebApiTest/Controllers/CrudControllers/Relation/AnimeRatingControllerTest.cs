using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.Relation.AnimeRatings;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Dtos.Dto.Relation.AnimeRatings;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.Relation;

namespace WebApiForHikka.Test.Controllers.CrudControllers.Relation;

public class AnimeRatingControllerTest : CrudControllerBaseTest<
    AnimeRatingController,
    AnimeRatingRelationService,
    AnimeRating,
    IAnimeRatingRelationRepository,
    UpdateAnimeRatingDto,
    CreateAnimeRatingDto,
    GetAnimeRatingDto,
    ReturnPageDto<GetAnimeRatingDto>
>
{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var repository = new AnimeRatingRelationRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        alternativeServices.AddSingleton(dbContext);

        return new AllServicesInController(new AnimeRatingRelationService(repository), userManager, roleManager);
    }

    protected override ICollection<AnimeRating> GetCollectionOfModels(int howMany)
    {
        ICollection<AnimeRating> seoAdditions = new List<AnimeRating>();
        for (var i = 0; i < howMany; ++i) seoAdditions.Add(GetModelSample());
        return seoAdditions;
    }

    protected override async Task<AnimeRatingController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        return new AnimeRatingController(
            allServicesInController.CrudService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager)
        );
    }

    protected override void MutationBeforeDtoCreation(CreateAnimeRatingDto createDto,
        AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
    }

    protected override void MutationBeforeDtoUpdate(UpdateAnimeRatingDto updateDto,
        AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
    }

    protected override CreateAnimeRatingDto GetCreateDtoSample()
    {
        return GetAnimeRatingModels.GetCreateDtoSample();
    }

    protected override GetAnimeRatingDto GetGetDtoSample()
    {
        return GetAnimeRatingModels.GetGetDtoSample();
    }

    protected override UpdateAnimeRatingDto GetUpdateDtoSample()
    {
        return GetAnimeRatingModels.GetUpdateDtoSample();
    }

    protected override AnimeRating GetModelSample()
    {
        return GetAnimeRatingModels.GetSample();
    }
}