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
using WebApiForHikka.SharedFunction.Helpers.LinkFactory;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;
using WebApiForHikka.WebApi.Helper.FileHelper;

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
        
        Mock<IFileHelper> fileHelperMock = new Mock<IFileHelper>();

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

    protected override async Task<AnimeBackdropController> GetController(AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        AllServicesInController allServices = allServicesInController;

        Mock<IFileHelper> fileHelperMock = new Mock<IFileHelper>();

        Mock<IColorHelper> colorHelperMock = new Mock<IColorHelper>();

        Mock<ILinkFactory> linkFactoryMock = new Mock<ILinkFactory>();

        fileHelperMock.Setup(m => m.UploadFileImage(It.IsAny<IFormFile>(), It.IsAny<string[]>()))
      .Returns("mocked/path/to/file");

        fileHelperMock.Setup(m => m.DeleteFile(It.IsAny<string[]>(), It.IsAny<string>()));

        fileHelperMock.Setup(m => m.OverrideFileImage(It.IsAny<IFormFile>(), It.IsAny<string>()));

        colorHelperMock.Setup(m => m.GetListOfColorsFromImage(It.IsAny<IFormFile>())).Returns([32131, 32342, 31341, 23421]);

        linkFactoryMock.Setup(
            m => m.GetLinkForDowloadImage(It.IsAny<HttpRequest>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>())).Returns("test/image/url");



        return new AnimeBackdropController(
            allServices.CrudService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager, allServicesInController.RoleManager),
            alternativeServices.GetRequiredService<IAnimeService>(),
            fileHelperMock.Object,
            colorHelperMock.Object,
            linkFactoryMock.Object

        );
    }

    protected override void MutationBeforeDtoCreation(CreateAnimeBackdropDto createDto, AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        var Anime = GetAnimeModels.GetModelSample();

        var animeService = alternativeServices.GetRequiredService<IAnimeService>();

        animeService.CreateAsync(Anime, CancellationToken).Wait();

        createDto.AnimeId = Anime.Id;
    }

    protected override void MutationBeforeDtoUpdate(UpdateAnimeBackdropDto updateDto, AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        var Anime = GetAnimeModels.GetSample();

        var animeService = alternativeServices.GetRequiredService<IAnimeService>();

        animeService.CreateAsync(Anime, CancellationToken).Wait();

        updateDto.AnimeId = Anime.Id;
    }

    protected override CreateAnimeBackdropDto GetCreateDtoSample() => GetAnimeBackdropModels.GetCreateSampleDto();
    protected override GetAnimeBackdropDto GetGetDtoSample() => GetAnimeBackdropModels.GetGetDtoSample();
    protected override UpdateAnimeBackdropDto GetUpdateDtoSample() => GetAnimeBackdropModels.GetUpdateDtoSample();
    protected override AnimeBackdrop GetModelSample() => GetAnimeBackdropModels.GetSample();
}