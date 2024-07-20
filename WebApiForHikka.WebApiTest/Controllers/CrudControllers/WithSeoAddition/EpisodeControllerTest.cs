using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithSeoAddition.Episodes;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Episodes;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithSeoAddition;

public class EpisodeControllerTest : CrudControllerBaseWithSeoAddition<
    EpisodeController,
    EpisodeService,
    Episode,
    IEpisodeRepository,
    UpdateEpisodeDto,
    CreateEpisodeDto,
    GetEpisodeDto,
    ReturnPageDto<GetEpisodeDto>
>
{
    protected override AllServicesInControllerWithSeoAddition GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var countryRepository = new EpisodeRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        return new AllServicesInControllerWithSeoAddition(new EpisodeService(countryRepository),
            new SeoAdditionService(seoAdditionRepository), userManager, roleManager);
    }

    protected override ICollection<Episode> GetCollectionOfModels(int howMany)
    {
        ICollection<Episode> seoAdditions = new List<Episode>();
        for (var i = 0; i < howMany; ++i) seoAdditions.Add(GetModelSample());
        return seoAdditions;
    }

    protected override async Task<EpisodeController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController as AllServicesInControllerWithSeoAddition ??
                          throw new Exception("method getController in EpisodeControllerTest");

        return new EpisodeController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager, allServices.RoleManager)
        );
    }


    protected override CreateEpisodeDto GetCreateDtoSample()
    {
        return GetEpisodeModels.GetCreateDtoSample();
    }

    protected override GetEpisodeDto GetGetDtoSample()
    {
        return GetEpisodeModels.GetGetDtoSample();
    }

    protected override Episode GetModelSample()
    {
        return GetEpisodeModels.GetSample();
    }

    protected override UpdateEpisodeDto GetUpdateDtoSample()
    {
        return GetEpisodeModels.GetUpdateDtoSample();
    }
}