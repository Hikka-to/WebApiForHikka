﻿using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeVideoKinds;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeVideos;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeVideos;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.WebApi.Controllers.ControllersWithoutSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithoutSeoAddition;

public class AnimeVideoControllerTest : CrudControllerBaseTest<
    AnimeVideoController,
    AnimeVideoService,
    AnimeVideo,
    IAnimeVideoRepository,
    UpdateAnimeVideoDto,
    CreateAnimeVideoDto,
    GetAnimeVideoDto,
    ReturnPageDto<GetAnimeVideoDto>
>
{
    protected override AllServicesInController GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var repository = new AnimeVideoRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        alternativeServices.AddSingleton(dbContext);
        alternativeServices.AddSingleton<IAnimeVideoKindRepository, AnimeVideoKindRepository>();
        alternativeServices.AddSingleton<IAnimeVideoKindService, AnimeVideoKindService>();

        return new AllServicesInController(new AnimeVideoService(repository), userManager, roleManager);
    }

    protected override async Task<AnimeVideoController> GetController(AllServicesInController allServicesInController,
        IServiceProvider alternativeServices)
    {
        var allServices = allServicesInController;

        return new AnimeVideoController(
            allServices.CrudService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager,
                allServicesInController.RoleManager),
            alternativeServices.GetRequiredService<IAnimeVideoKindService>()
        );
    }

    protected override void MutationBeforeDtoCreation(CreateAnimeVideoDto createDto,
        AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        var animeVideoKind = GetAnimeVideoKindModels.GetSample();

        var animeVideoKindService = alternativeServices.GetRequiredService<IAnimeVideoKindService>();

        animeVideoKindService.CreateAsync(animeVideoKind, CancellationToken).Wait();

        createDto.AnimeVideoKindId = animeVideoKind.Id;
    }

    protected override void MutationBeforeDtoUpdate(UpdateAnimeVideoDto updateDto,
        AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        var animeVideoKind = GetAnimeVideoKindModels.GetSample();

        var animeVideoKindService = alternativeServices.GetRequiredService<IAnimeVideoKindService>();

        animeVideoKindService.CreateAsync(animeVideoKind, CancellationToken).Wait();

        updateDto.AnimeVideoKindId = animeVideoKind.Id;
    }

    protected override CreateAnimeVideoDto GetCreateDtoSample()
    {
        return GetAnimeVideoModels.GetCreateDtoSample();
    }

    protected override GetAnimeVideoDto GetGetDtoSample()
    {
        return GetAnimeVideoModels.GetGetDtoSample();
    }

    protected override UpdateAnimeVideoDto GetUpdateDtoSample()
    {
        return GetAnimeVideoModels.GetUpdateDtoSample();
    }

    protected override AnimeVideo GetModelSample()
    {
        return GetAnimeVideoModels.GetSample();
    }
}