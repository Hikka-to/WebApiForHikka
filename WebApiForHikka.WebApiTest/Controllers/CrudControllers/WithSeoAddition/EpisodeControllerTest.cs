using Microsoft.Extensions.DependencyInjection;
using Moq;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithoutSeoAddition.EpisodeImages;
using WebApiForHikka.Application.WithSeoAddition.Episodes;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Episodes;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Controllers.Shared;
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
        Mock<IFileHelper> fileHelperMock = new();

        fileHelperMock.Setup(m => m.DeleteFile(It.IsAny<string[]>(), It.IsAny<string>()));

        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var countryRepository = new EpisodeRepository(dbContext);
        var episodeImagesRepository = new EpisodeImageRepository(dbContext);
        var episodeImagesService = new EpisodeImageService(episodeImagesRepository, fileHelperMock.Object);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);


        return new AllServicesInControllerWithSeoAddition(new EpisodeService(countryRepository, episodeImagesService),
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