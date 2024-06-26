using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Controller.Shared;
using WebApiForHikka.Test.Controllers.CrudControllers.WithSeoAddition;
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

        var repository = new AnimeBackdropRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        alternativeServices.AddSingleton(dbContext);
        alternativeServices.AddSingleton<IAnimeRepository, AnimeRepository>();
        alternativeServices.AddSingleton<IAnimeService, AnimeService>();

        return new AllServicesInController(new AnimeBackdropService(repository), userManager, roleManager);
    }

    protected override async Task<AnimeBackdropController> GetController(AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        AllServicesInController allServices = allServicesInController;

        return new AnimeBackdropController(
            allServices.CrudService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager, allServicesInController.RoleManager),
            alternativeServices.GetRequiredService<IAnimeService>()
        );
    }

    protected override void MutationBeforeDtoCreation(CreateAnimeBackdropDto createDto, AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        var anime = new AnimeControllerTest().Anime;

        var animeService = alternativeServices.GetRequiredService<IAnimeService>();

        animeService.CreateAsync(anime, CancellationToken).Wait();

        createDto.AnimeId = anime.Id;
    }

    protected override void MutationBeforeDtoUpdate(UpdateAnimeBackdropDto updateDto, AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        var anime = new AnimeControllerTest().Anime;

        var animeService = alternativeServices.GetRequiredService<IAnimeService>();

        animeService.CreateAsync(anime, CancellationToken).Wait();

        updateDto.AnimeId = anime.Id;
    }

    protected override CreateAnimeBackdropDto GetCreateDtoSample() => new()
    {
        AnimeId = Guid.NewGuid(),
        Path = Faker.Lorem.GetFirstWord(),
        Width = Faker.RandomNumber.Next(),
        Height = Faker.RandomNumber.Next(),
        Colors = [Faker.RandomNumber.Next(), Faker.RandomNumber.Next(), Faker.RandomNumber.Next()]
    };

    protected override GetAnimeBackdropDto GetGetDtoSample() => new()
    {
        AnimeId = Guid.NewGuid(),
        Path = Faker.Lorem.GetFirstWord(),
        Width = Faker.RandomNumber.Next(),
        Height = Faker.RandomNumber.Next(),
        Colors = [Faker.RandomNumber.Next(), Faker.RandomNumber.Next(), Faker.RandomNumber.Next()],
        Id = Guid.NewGuid()
    };

    protected override UpdateAnimeBackdropDto GetUpdateDtoSample() => new()
    {
        AnimeId = Guid.NewGuid(),
        Path = Faker.Lorem.GetFirstWord(),
        Width = Faker.RandomNumber.Next(),
        Height = Faker.RandomNumber.Next(),
        Colors = [Faker.RandomNumber.Next(), Faker.RandomNumber.Next(), Faker.RandomNumber.Next()],
        Id = Guid.NewGuid()
    };

    protected override AnimeBackdrop GetModelSample() => new()
    {
        Anime = new AnimeControllerTest().Anime,
        Path = Faker.Lorem.GetFirstWord(),
        Width = Faker.RandomNumber.Next(),
        Height = Faker.RandomNumber.Next(),
        Colors = [Faker.RandomNumber.Next(), Faker.RandomNumber.Next(), Faker.RandomNumber.Next()]
    };
}
