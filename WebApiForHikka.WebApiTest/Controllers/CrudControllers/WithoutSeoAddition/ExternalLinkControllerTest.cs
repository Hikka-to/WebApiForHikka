using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.WithoutSeoAddition.ExternalLinks;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.ExternalLinks;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithoutSeoAddition;

public class ExternalLinkControllerTest : CrudControllerBaseTest<
    ExternalLinkController,
    ExternalLinkService,
    ExternalLink,
    IExternalLinkRepository,
    UpdateExternalLinkDto,
    CreateExternalLinkDto,
    GetExternalLinkDto,
    ReturnPageDto<GetExternalLinkDto>
>
{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var repository = new ExternalLinkRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        alternativeServices.AddSingleton(dbContext);
        alternativeServices.AddSingleton<IAnimeRepository, AnimeRepository>();
        alternativeServices.AddSingleton<IAnimeService, AnimeService>();

        return new AllServicesInController(new ExternalLinkService(repository), userManager, roleManager);
    }

    protected override async Task<ExternalLinkController> GetController(
        AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        return new ExternalLinkController(
            allServicesInController.CrudService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager),
            alternativeServices.GetRequiredService<IAnimeService>()
        );
    }

    protected override void MutationBeforeDtoCreation(CreateExternalLinkDto createDto,
        AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        var anime = GetAnimeModels.GetSample();

        var animeService = alternativeServices.GetRequiredService<IAnimeService>();

        animeService.CreateAsync(anime, CancellationToken).Wait();

        createDto.AnimeId = anime.Id;
    }

    protected override void MutationBeforeDtoUpdate(UpdateExternalLinkDto updateDto,
        AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var anime = GetAnimeModels.GetSample();

        var animeService = alternativeServices.GetRequiredService<IAnimeService>();

        animeService.CreateAsync(anime, CancellationToken).Wait();

        updateDto.AnimeId = anime.Id;
    }

    protected override CreateExternalLinkDto GetCreateDtoSample()
    {
        return GetExternalLinkModels.GetCreateDtoSample();
    }

    protected override GetExternalLinkDto GetGetDtoSample()
    {
        return GetExternalLinkModels.GetGetDtoSample();
    }

    protected override UpdateExternalLinkDto GetUpdateDtoSample()
    {
        return GetExternalLinkModels.GetUpdateDtoSample();
    }

    protected override ExternalLink GetModelSample()
    {
        return GetExternalLinkModels.GetSample();
    }
}