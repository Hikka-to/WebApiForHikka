using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeBackdrops;
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

public class AnimeBackdropControllerTest : CrudControllerBaseTest<
    AnimeBackdropController,
    AnimeBackdropService,
    AnimeBackdrop,
    IAnimeBackdropRepository,
    UpdateAnimeBackdropDto,
    CreateAnimeBackdropDto,
    GetAnimeBackdropDto,
    ReturnPageDto<GetAnimeBackdropDto>
>

{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var fileHelperMock = new Mock<IFileHelper>();

        fileHelperMock.Setup(m => m.DeleteFile(It.IsAny<string[]>(), It.IsAny<string>()));


        var repository = new AnimeBackdropRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        alternativeServices.AddSingleton(dbContext);
        alternativeServices.AddSingleton<IAnimeRepository, AnimeRepository>();
        alternativeServices.AddSingleton<IAnimeService, AnimeService>();

        alternativeServices.AddSingleton<IAnimeBackdropRepository, AnimeBackdropRepository>();
        alternativeServices.AddSingleton<IAnimeBackdropService, AnimeBackdropService>();

        alternativeServices.AddSingleton<IFileHelper, FileHelper>();


        return new AllServicesInController(new AnimeBackdropService(repository, fileHelperMock.Object),
            userManager,
            roleManager
        );
    }

    protected override async Task<AnimeBackdropController> GetController(
        AllServicesInController allServicesInController, IServiceProvider alternativeServices)
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
            m => m.GetLinkForDowloadImage(It.IsAny<HttpRequest>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>())).Returns("test/image/url");


        return new AnimeBackdropController(
            allServices.CrudService,
            Mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager),
            alternativeServices.GetRequiredService<IAnimeService>(),
            fileHelperMock.Object,
            colorHelperMock.Object,
            linkFactoryMock.Object
        );
    }

    protected override void MutationBeforeDtoCreation(CreateAnimeBackdropDto createDto,
        AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        var anime = GetAnimeModels.GetModelSample();

        var animeService = alternativeServices.GetRequiredService<IAnimeService>();

        animeService.CreateAsync(anime, CancellationToken).Wait();

        createDto.AnimeId = anime.Id;
    }

    protected override void MutationBeforeDtoUpdate(UpdateAnimeBackdropDto updateDto,
        AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        var anime = GetAnimeModels.GetSample();

        var animeService = alternativeServices.GetRequiredService<IAnimeService>();

        animeService.CreateAsync(anime, CancellationToken).Wait();

        updateDto.AnimeId = anime.Id;
    }

    protected override CreateAnimeBackdropDto GetCreateDtoSample()
    {
        return GetAnimeBackdropModels.GetCreateSampleDto();
    }

    protected override GetAnimeBackdropDto GetGetDtoSample()
    {
        return GetAnimeBackdropModels.GetGetDtoSample();
    }

    protected override UpdateAnimeBackdropDto GetUpdateDtoSample()
    {
        return GetAnimeBackdropModels.GetUpdateDtoSample();
    }

    protected override AnimeBackdrop GetModelSample()
    {
        return GetAnimeBackdropModels.GetSample();
    }
}