using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WebApiForHikka.Application.WithoutSeoAddition.EpisodeImages;
using WebApiForHikka.Application.WithSeoAddition.Episodes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Episodes;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedFunction.Helpers.ColorHelper;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;
using WebApiForHikka.SharedFunction.Helpers.LinkFactory;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithoutSeoAddition;

public class EpisodeImageControllerTest : CrudControllerBaseTest<
    EpisodeImageController,
    EpisodeImageService,
    EpisodeImage,
    IEpisodeImageRepository,
    UpdateEpisodeImageDto,
    CreateEpisodeImageDto,
    GetEpisodeImageDto,
    ReturnPageDto<GetEpisodeImageDto>
>

{
    protected override AllServicesInController GetAllServices(
        IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();


        var repository = new EpisodeImageRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        alternativeServices.AddSingleton(dbContext);
        alternativeServices.AddSingleton<IEpisodeRepository, EpisodeRepository>();
        alternativeServices.AddSingleton<IEpisodeService, EpisodeService>();

        alternativeServices.AddSingleton<IEpisodeImageRepository, EpisodeImageRepository>();
        alternativeServices.AddSingleton<IEpisodeImageService, EpisodeImageService>();

        alternativeServices.AddSingleton<IFileHelper, FileHelper>();

        var fileHelperMock = new Mock<IFileHelper>();

        fileHelperMock.Setup(m => m.DeleteFile(It.IsAny<string[]>(), It.IsAny<string>()));


        return new AllServicesInController(
            new EpisodeImageService(repository, fileHelperMock.Object),
            userManager,
            roleManager
        );
    }

    protected override async Task<EpisodeImageController> GetController(
        AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController;

        var fileHelperMock = new Mock<IFileHelper>();

        var colorHelperMock = new Mock<IColorHelper>();

        var linkFactoryMock = new Mock<ILinkFactory>();

        fileHelperMock.Setup(m => m.UploadFileImage(It.IsAny<IFormFile>(), It.IsAny<string[]>()))
            .Returns("mocked/path/to/file");

        fileHelperMock.Setup(m => m.DeleteFile(It.IsAny<string[]>(), It.IsAny<string>()));

        fileHelperMock.Setup(m => m.OverrideFileImage(It.IsAny<IFormFile>(), It.IsAny<string>()));

        colorHelperMock.Setup(m => m.GetListOfColorsFromImage(It.IsAny<IFormFile>()))
            .Returns([32131, 32342, 31341, 23421]);

        linkFactoryMock.Setup(
            m => m.GetLinkForDownloadImage(It.IsAny<HttpRequest>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>())).Returns("test/image/url");


        return new EpisodeImageController(
            allServices.CrudService,
            Mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager),
            alternativeServices.GetRequiredService<IEpisodeService>(),
            fileHelperMock.Object,
            colorHelperMock.Object,
            linkFactoryMock.Object
        );
    }

    protected override void MutationBeforeDtoCreation(CreateEpisodeImageDto createDto,
        AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        var episode = GetEpisodeModels.GetSample();

        var animeService = alternativeServices.GetRequiredService<IEpisodeService>();

        animeService.CreateAsync(episode, CancellationToken).Wait();

        createDto.EpisodeId = episode.Id;
    }

    protected override void MutationBeforeDtoUpdate(UpdateEpisodeImageDto updateDto,
        AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        var episode = GetEpisodeModels.GetSample();

        var animeService = alternativeServices.GetRequiredService<IEpisodeService>();

        animeService.CreateAsync(episode, CancellationToken).Wait();

        updateDto.EpisodeId = episode.Id;
    }

    protected override CreateEpisodeImageDto GetCreateDtoSample()
    {
        return GetEpisodeImageModels.GetCreateSampleDto();
    }

    protected override GetEpisodeImageDto GetGetDtoSample()
    {
        return GetEpisodeImageModels.GetGetDtoSample();
    }

    protected override UpdateEpisodeImageDto GetUpdateDtoSample()
    {
        return GetEpisodeImageModels.GetUpdateDtoSample();
    }

    protected override EpisodeImage GetModelSample()
    {
        return GetEpisodeImageModels.GetSample();
    }
}