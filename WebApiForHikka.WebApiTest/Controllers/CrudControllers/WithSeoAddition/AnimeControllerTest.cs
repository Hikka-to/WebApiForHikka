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
using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Animes;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedFunction.Helpers.ColorHelper;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.MyDataFaker;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition.Animes;
using WebApiForHikka.WebApi.Helper.FileHelper;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithSeoAddition;

//public class AnimeControllerTest : CrudControllerBaseWithSeoAddition<
//    AnimeController,
//    AnimeService,
//    Anime,
//    IAnimeRepository,
//    UpdateAnimeDto,
//    CreateAnimeDto,
//    GetAnimeDto,
//    ReturnPageDto<GetAnimeDto>
//    >
//{
//    public Anime Anime => GetModelSample();

//    protected override AllServicesInControllerWithSeoAddition GetAllServices(IServiceCollection alternativeServices)
//    {
//        var dbContext = GetDatabaseContext();

//        var seoAdditionRepository = new SeoAdditionRepository(dbContext);
//        var animeRepository = new AnimeRepository(dbContext);
//        var userManager = GetUserManager(dbContext);
//        var roleManager = GetRoleManager(dbContext);

//        alternativeServices.AddSingleton(dbContext);
//        alternativeServices.AddSingleton<IKindRepository, KindRepository>();
//        alternativeServices.AddSingleton<IStatusRepository, StatusRepository>();
//        alternativeServices.AddSingleton<IPeriodRepository, PeriodRepository>();
//        alternativeServices.AddSingleton<IRestrictedRatingRepository, RestrictedRatingRepository>();
//        alternativeServices.AddSingleton<ISourceRepository, SourceRepository>();
//        alternativeServices.AddSingleton<ITagRepository, TagRepository>();
//        alternativeServices.AddSingleton<IKindService, KindService>();
//        alternativeServices.AddSingleton<IStatusService, StatusService>();
//        alternativeServices.AddSingleton<IPeriodService, PeriodService>();
//        alternativeServices.AddSingleton<IRestrictedRatingService, RestrictedRatingService>();
//        alternativeServices.AddSingleton<ISourceService, SourceService>();
//        alternativeServices.AddSingleton<ITagService, TagService>();

//        alternativeServices.AddSingleton<IColorHelper, ColorHelper>();

//        return new AllServicesInControllerWithSeoAddition(new AnimeService(animeRepository), new SeoAdditionService(seoAdditionRepository), userManager, roleManager);
//    }

//    protected override async Task<AnimeController> GetController(AllServicesInController allServicesInController, IServiceProvider alternativeServices)
//    {
//        AllServicesInControllerWithSeoAddition allServices = allServicesInController as AllServicesInControllerWithSeoAddition ?? throw new Exception("method getController in AnimeControllerTest");


//        Mock<IFileHelper> fileHelperMock = new Mock<IFileHelper>();

//        Mock<IColorHelper> colorHelperMock = new Mock<IColorHelper>();

//        fileHelperMock.Setup(m => m.UploadFileImage(It.IsAny<IFormFile>(), It.IsAny<string[]>()))
//             .Returns("mocked/path/to/file");

//        fileHelperMock.Setup(m => m.DeleteFile(It.IsAny<string[]>(), It.IsAny<string>()));

//        fileHelperMock.Setup(m => m.OverrideFileImage(It.IsAny<IFormFile>(), It.IsAny<string>()));

//        colorHelperMock.Setup(m => m.GetListOfColorsFromImage(It.IsAny<IFormFile>())).Returns([32131, 32342, 31341, 23421]);



//        return new AnimeController(
//            allServices.CrudService,
//            allServices.SeoAdditionService,
//            _mapper,
//            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager, allServicesInController.RoleManager),
//            alternativeServices.GetRequiredService<IKindService>(),
//            alternativeServices.GetRequiredService<IStatusService>(),
//            alternativeServices.GetRequiredService<IPeriodService>(),
//            alternativeServices.GetRequiredService<IRestrictedRatingService>(),
//            alternativeServices.GetRequiredService<ISourceService>(),
//            alternativeServices.GetRequiredService<ITagService>(),
//            fileHelperMock.Object,
//            colorHelperMock.Object
//        );
//    }

//    protected override void MutationBeforeDtoCreation(CreateAnimeDto createDto, AllServicesInController allServicesInController, IServiceProvider alternativeServices)
//    {
//        var kind = GetKindModels.GetSample();
//        var status = GetStatusModels.GetSample();
//        var period = GetPeriodModels.GetSample();
//        var restrictedRating = GetRestrictedRatingModels.GetSample();
//        var source = GetSourceModels.GetSample();
//        var tag = GetTagModels.GetSample();

//        var kindService = alternativeServices.GetRequiredService<IKindService>();
//        var statusService = alternativeServices.GetRequiredService<IStatusService>();
//        var periodService = alternativeServices.GetRequiredService<IPeriodService>();
//        var restrictedRatingService = alternativeServices.GetRequiredService<IRestrictedRatingService>();
//        var sourceService = alternativeServices.GetRequiredService<ISourceService>();
//        var tagService = alternativeServices.GetRequiredService<ITagService>();

//        kindService.CreateAsync(kind, CancellationToken).Wait();
//        statusService.CreateAsync(status, CancellationToken).Wait();
//        periodService.CreateAsync(period, CancellationToken).Wait();
//        restrictedRatingService.CreateAsync(restrictedRating, CancellationToken).Wait();
//        sourceService.CreateAsync(source, CancellationToken).Wait();
//        tagService.CreateAsync(tag, CancellationToken).Wait();


//        createDto.KindId = kind.Id;
//        createDto.StatusId = status.Id;
//        createDto.PeriodId = period.Id;
//        createDto.RestrictedRatingId = restrictedRating.Id;
//        createDto.SourceId = source.Id;
//        createDto.Tags = [tag.Id];
//    }

//    protected override void MutationBeforeDtoUpdate(UpdateAnimeDto updateDto, AllServicesInController allServicesInController, IServiceProvider alternativeServices)
//    {
//        var kind = GetKindModels.GetSample();

//        var status = GetStatusModels.GetSample();

//        var period = GetPeriodModels.GetSample();

//        var restrictedRating = GetRestrictedRatingModels.GetSample();

//        var source = GetSourceModels.GetSample();

//        var kindService = alternativeServices.GetRequiredService<IKindService>();
//        var statusService = alternativeServices.GetRequiredService<IStatusService>();
//        var periodService = alternativeServices.GetRequiredService<IPeriodService>();
//        var restrictedRatingService = alternativeServices.GetRequiredService<IRestrictedRatingService>();
//        var sourceService = alternativeServices.GetRequiredService<ISourceService>();

//        kindService.CreateAsync(kind, CancellationToken).Wait();
//        statusService.CreateAsync(status, CancellationToken).Wait();
//        periodService.CreateAsync(period, CancellationToken).Wait();
//        restrictedRatingService.CreateAsync(restrictedRating, CancellationToken).Wait();
//        sourceService.CreateAsync(source, CancellationToken).Wait();

//        updateDto.KindId = kind.Id;
//        updateDto.StatusId = status.Id;
//        updateDto.PeriodId = period.Id;
//        updateDto.RestrictedRatingId = restrictedRating.Id;
//        updateDto.SourceId = source.Id;
//    }

//    protected override CreateAnimeDto GetCreateDtoSample() => GetAnimeModels.GetCreateDtoSample();


//    protected override GetAnimeDto GetGetDtoSample() => GetAnimeModels.GetGetDtoSample();
//    protected override Anime GetModelSample() => GetAnimeModels.GetModelSample();

//    protected override UpdateAnimeDto GetUpdateDtoSample() => GetAnimeModels.GetUpdateDtoSample();

//}