using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Application.WithSeoAddition.Characters;
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Characters;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedFunction.Helpers.ColorHelper;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;
using WebApiForHikka.SharedFunction.Helpers.LinkFactory;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition.Characters;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithSeoAddition;

public class CharacterControllerTest : CrudControllerBaseWithSeoAddition<
    CharacterController,
    CharacterService,
    Character,
    ICharacterRepository,
    UpdateCharacterDto,
    CreateCharacterDto,
    GetCharacterDto,
    ReturnPageDto<GetCharacterDto>
>
{
    protected override AllServicesInControllerWithSeoAddition GetAllServices(
        IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var characterRepository = new CharacterRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);


        alternativeServices.AddSingleton(dbContext);
        alternativeServices.AddSingleton<ITagRepository, TagRepository>();
        alternativeServices.AddSingleton<ITagService, TagService>();

        alternativeServices.AddSingleton<IAnimeBackdropRepository, AnimeBackdropRepository>();
        alternativeServices.AddSingleton<IAnimeBackdropService, AnimeBackdropService>();

        alternativeServices.AddSingleton<IFileHelper, FileHelper>();

        alternativeServices.AddSingleton<IAnimeRepository, AnimeRepository>();
        alternativeServices.AddSingleton<IAnimeService, AnimeService>();


        Mock<IFileHelper> fileHelperMock = new();

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

        return new AllServicesInControllerWithSeoAddition(
            new CharacterService(characterRepository, fileHelperMock.Object),
            new SeoAdditionService(seoAdditionRepository), userManager, roleManager);
    }

    protected override async Task<CharacterController> GetController(
        AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController as AllServicesInControllerWithSeoAddition ??
                          throw new Exception("method getController in CharacterControllerTest");

        Mock<IFileHelper> fileHelperMock = new();

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


        return new CharacterController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            alternativeServices.GetRequiredService<ITagService>(),
            alternativeServices.GetRequiredService<IAnimeService>(),
            fileHelperMock.Object,
            linkFactoryMock.Object,
            Mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager)
        );
    }

    protected override CreateCharacterDto GetCreateDtoSample()
    {
        return GetCharacterModels.GetCreateDtoSample();
    }

    protected override GetCharacterDto GetGetDtoSample()
    {
        return GetCharacterModels.GetGetDtoSample();
    }

    protected override Character GetModelSample()
    {
        return GetCharacterModels.GetModelSample();
    }

    protected override UpdateCharacterDto GetUpdateDtoSample()
    {
        return GetCharacterModels.GetUpdateDtoSample();
    }
}