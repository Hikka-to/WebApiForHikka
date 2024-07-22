using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WebApiForHikka.Application.Kinds;
using WebApiForHikka.Application.Periods;
using WebApiForHikka.Application.RestrictedRatings;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.Sources;
using WebApiForHikka.Application.Statuses;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Application.WithSeoAddition.Countries;
using WebApiForHikka.Application.WithSeoAddition.Dubs;
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Animes;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedFunction.Helpers.ColorHelper;
using WebApiForHikka.SharedFunction.Helpers.LinkFactory;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition.Animes;
using WebApiForHikka.WebApi.Helper.FileHelper;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithSeoAddition;

public class AnimeControllerTest : CrudControllerBaseWithSeoAddition<
    AnimeController,
    AnimeService,
    Anime,
    IAnimeRepository,
    UpdateAnimeDto,
    CreateAnimeDto,
    GetAnimeDto,
    ReturnPageDto<GetAnimeDto>
    >
{
    public Anime Anime => GetModelSample();

    protected override AllServicesInControllerWithSeoAddition GetAllServices(IServiceCollection alternativeServices)
    {
        var dbContext = GetDatabaseContext();

        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
        var animeRepository = new AnimeRepository(dbContext);
        var userManager = GetUserManager(dbContext);
        var roleManager = GetRoleManager(dbContext);

        alternativeServices.AddSingleton(dbContext);
        alternativeServices.AddSingleton<IKindRepository, KindRepository>();
        alternativeServices.AddSingleton<IStatusRepository, StatusRepository>();
        alternativeServices.AddSingleton<IPeriodRepository, PeriodRepository>();
        alternativeServices.AddSingleton<IRestrictedRatingRepository, RestrictedRatingRepository>();
        alternativeServices.AddSingleton<ISourceRepository, SourceRepository>();
        alternativeServices.AddSingleton<ITagRepository, TagRepository>();
        alternativeServices.AddSingleton<ICountryRepository, CountryRepository>();
        alternativeServices.AddSingleton<IDubRepository, DubRepository>();
        alternativeServices.AddSingleton<IAnimeRepository, AnimeRepository>();

        alternativeServices.AddSingleton<IKindService, KindService>();
        alternativeServices.AddSingleton<IStatusService, StatusService>();
        alternativeServices.AddSingleton<IPeriodService, PeriodService>();
        alternativeServices.AddSingleton<IRestrictedRatingService, RestrictedRatingService>();
        alternativeServices.AddSingleton<ISourceService, SourceService>();
        alternativeServices.AddSingleton<ITagService, TagService>();
        alternativeServices.AddSingleton<ICountryService, CountryService>();
        alternativeServices.AddSingleton<IDubService, DubService>();
        alternativeServices.AddSingleton<IAnimeService, AnimeService>();

        alternativeServices.AddSingleton<IColorHelper, ColorHelper>();

        return new AllServicesInControllerWithSeoAddition(new AnimeService(animeRepository), new SeoAdditionService(seoAdditionRepository), userManager, roleManager);
    }

    protected override async Task<AnimeController> GetController(AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        AllServicesInControllerWithSeoAddition allServices = allServicesInController as AllServicesInControllerWithSeoAddition ?? throw new Exception("method getController in AnimeControllerTest");


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



        return new AnimeController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager, allServicesInController.RoleManager),
            alternativeServices.GetRequiredService<IKindService>(),
            alternativeServices.GetRequiredService<IStatusService>(),
            alternativeServices.GetRequiredService<IPeriodService>(),
            alternativeServices.GetRequiredService<IRestrictedRatingService>(),
            alternativeServices.GetRequiredService<ISourceService>(),
            alternativeServices.GetRequiredService<ITagService>(),
            alternativeServices.GetRequiredService<ICountryService>(),
            alternativeServices.GetRequiredService<IDubService>(),
            fileHelperMock.Object,
            colorHelperMock.Object,
            linkFactoryMock.Object
        );
    }

    protected override void MutationBeforeDtoCreation(CreateAnimeDto createDto, AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        var kind = GetKindModels.GetSample();
        var status = GetStatusModels.GetSample();
        var period = GetPeriodModels.GetSample();
        var restrictedRating = GetRestrictedRatingModels.GetSample();
        var source = GetSourceModels.GetSample();
        var tag = GetTagModels.GetSample();
        var country = GetCountryModels.GetSample();
        var dub = GetDubModels.GetSample();
        var anime = GetAnimeModels.GetSampleWithoutManyToMany();

        var kindService = alternativeServices.GetRequiredService<IKindService>();
        var statusService = alternativeServices.GetRequiredService<IStatusService>();
        var periodService = alternativeServices.GetRequiredService<IPeriodService>();
        var restrictedRatingService = alternativeServices.GetRequiredService<IRestrictedRatingService>();
        var sourceService = alternativeServices.GetRequiredService<ISourceService>();
        var tagService = alternativeServices.GetRequiredService<ITagService>();
        var countryService = alternativeServices.GetRequiredService<ICountryService>();
        var dubService = alternativeServices.GetRequiredService<IDubService>();
        var animeService = alternativeServices.GetRequiredService<IAnimeService>();

        kindService.CreateAsync(kind, CancellationToken).Wait();
        statusService.CreateAsync(status, CancellationToken).Wait();
        periodService.CreateAsync(period, CancellationToken).Wait();
        restrictedRatingService.CreateAsync(restrictedRating, CancellationToken).Wait();
        sourceService.CreateAsync(source, CancellationToken).Wait();
        tagService.CreateAsync(tag, CancellationToken).Wait();
        countryService.CreateAsync(country, CancellationToken).Wait();
        dubService.CreateAsync(dub, CancellationToken).Wait();
        animeService.CreateAsync(anime, CancellationToken).Wait();


        createDto.KindId = kind.Id;
        createDto.StatusId = status.Id;
        createDto.PeriodId = period.Id;
        createDto.RestrictedRatingId = restrictedRating.Id;
        createDto.SourceId = source.Id;
        createDto.Tags = [tag.Id];
        createDto.Dubs = [dub.Id];
        createDto.Countries = [country.Id];
        createDto.SimilarAnimes = [anime.Id];
    }

    protected override void MutationBeforeDtoUpdate(UpdateAnimeDto updateDto, AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        var kind = GetKindModels.GetSample();
        var status = GetStatusModels.GetSample();
        var period = GetPeriodModels.GetSample();
        var restrictedRating = GetRestrictedRatingModels.GetSample();
        var source = GetSourceModels.GetSample();
        var country = GetCountryModels.GetSample();
        var tag = GetTagModels.GetSample();
        var dub = GetDubModels.GetSample();
        var anime = GetAnimeModels.GetSampleWithoutManyToMany();

        var kindService = alternativeServices.GetRequiredService<IKindService>();
        var statusService = alternativeServices.GetRequiredService<IStatusService>();
        var periodService = alternativeServices.GetRequiredService<IPeriodService>();
        var restrictedRatingService = alternativeServices.GetRequiredService<IRestrictedRatingService>();
        var sourceService = alternativeServices.GetRequiredService<ISourceService>();
        var countryService = alternativeServices.GetRequiredService<ICountryService>();
        var tagService = alternativeServices.GetRequiredService<ITagService>();
        var dubService = alternativeServices.GetRequiredService<IDubService>();
        var animeService = alternativeServices.GetRequiredService<IAnimeService>();

        countryService.CreateAsync(country, CancellationToken).Wait();
        dubService.CreateAsync(dub, CancellationToken).Wait();
        kindService.CreateAsync(kind, CancellationToken).Wait();
        statusService.CreateAsync(status, CancellationToken).Wait();
        periodService.CreateAsync(period, CancellationToken).Wait();
        tagService.CreateAsync(tag, CancellationToken).Wait();
        restrictedRatingService.CreateAsync(restrictedRating, CancellationToken).Wait();
        sourceService.CreateAsync(source, CancellationToken).Wait();
        animeService.CreateAsync(anime, CancellationToken).Wait();

       
        updateDto.KindId = kind.Id;
        updateDto.StatusId = status.Id;
        updateDto.PeriodId = period.Id;
        updateDto.RestrictedRatingId = restrictedRating.Id;
        updateDto.SourceId = source.Id;
        updateDto.Tags = [tag.Id];
        updateDto.Dubs = [dub.Id];
        updateDto.Countries = [country.Id];
        updateDto.SimilarAnimes = [anime.Id];

    }

    protected override CreateAnimeDto GetCreateDtoSample() => GetAnimeModels.GetCreateDtoSample();


    protected override GetAnimeDto GetGetDtoSample() => GetAnimeModels.GetGetDtoSample();
    protected override Anime GetModelSample() => GetAnimeModels.GetModelSample();

    protected override UpdateAnimeDto GetUpdateDtoSample() => GetAnimeModels.GetUpdateDtoSample();

}