using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.WithoutSeoAddition.EpisodeImages;
using WebApiForHikka.Application.WithoutSeoAddition.Mediaplayers;
using WebApiForHikka.Application.WithSeoAddition.Episodes;
using WebApiForHikka.Application.WithSeoAddition.Formats;
using WebApiForHikka.Application.WithSeoAddition.LanguageMediaplayers;
using WebApiForHikka.Application.WithSeoAddition.Languages;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.LanguageMediaplayers;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedFunction.Helpers.FileHelper;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithSeoAddition;

public class LanguageMediaplayerControllerTest : CrudControllerBaseWithSeoAddition<
    LanguageMediaplayerController,
    LanguageMediaplayerService,
    LanguageMediaplayer,
    ILanguageMediaplayerRepository,
    UpdateLanguageMediaplayerDto,
    CreateLanguageMediaplayerDto,
    GetLanguageMediaplayerDto,
    ReturnPageDto<GetLanguageMediaplayerDto>
>
{
    protected override AllServicesInControllerWithSeoAddition GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();


        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var formatRepository = new LanguageMediaplayerRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        alternativeServices.AddSingleton(dbContext);
        alternativeServices.AddSingleton<ILanguageRepository, LanguageRepository>();
        alternativeServices.AddSingleton<IEpisodeRepository, EpisodeRepository>();
        alternativeServices.AddSingleton<IMediaplayerRepository, MediaplayerRepository>();
        alternativeServices.AddSingleton<IFormatRepository, FormatRepository>();
        alternativeServices.AddSingleton<IEpisodeImageRepository, EpisodeImageRepository>();


        alternativeServices.AddSingleton<ILanguageService, LanguageService>();
        alternativeServices.AddSingleton<IEpisodeService, EpisodeService>();
        alternativeServices.AddSingleton<IMediaplayerService, MediaplayerService>();
        alternativeServices.AddSingleton<IFormatService, FormatService>();
        alternativeServices.AddSingleton<IEpisodeImageService, EpisodeImageService>();

        alternativeServices.AddSingleton<IFileHelper, FileHelper>();


        return new AllServicesInControllerWithSeoAddition(new LanguageMediaplayerService(formatRepository),
            new SeoAdditionService(seoAdditionRepository), userManager, roleManager);
    }

    protected override async Task<LanguageMediaplayerController> GetController(
        AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController as AllServicesInControllerWithSeoAddition ??
                          throw new Exception("method getController in LanguageControllerTest");

        return new LanguageMediaplayerController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager),
            alternativeServices.GetRequiredService<IMediaplayerService>(),
            alternativeServices.GetRequiredService<ILanguageService>(),
            alternativeServices.GetRequiredService<IEpisodeService>(),
            alternativeServices.GetRequiredService<IFormatService>()
        );
    }

    protected override CreateLanguageMediaplayerDto GetCreateDtoSample()
    {
        return GetLanguageMediaplayerModels.GetCreateDtoSample();
    }

    protected override GetLanguageMediaplayerDto GetGetDtoSample()
    {
        return GetLanguageMediaplayerModels.GetGetDtoSample();
    }

    protected override LanguageMediaplayer GetModelSample()
    {
        return GetLanguageMediaplayerModels.GetSample();
    }

    protected override UpdateLanguageMediaplayerDto GetUpdateDtoSample()
    {
        return GetLanguageMediaplayerModels.GetUpdateDtoSample();
    }
}