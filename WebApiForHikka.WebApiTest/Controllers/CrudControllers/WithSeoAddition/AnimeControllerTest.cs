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
//        alternativeServices.AddSingleton<KindService>();
//        alternativeServices.AddSingleton<StatusService>();
//        alternativeServices.AddSingleton<PeriodService>();
//        alternativeServices.AddSingleton<RestrictedRatingService>();
//        alternativeServices.AddSingleton<SourceService>();
//        alternativeServices.AddSingleton<FileHelper>();

//        return new AllServicesInControllerWithSeoAddition(new AnimeService(animeRepository), new SeoAdditionService(seoAdditionRepository), userManager, roleManager);
//    }

//    protected override async Task<AnimeController> GetController(AllServicesInController allServicesInController, IServiceProvider alternativeServices)
//    {
//        AllServicesInControllerWithSeoAddition allServices = allServicesInController as AllServicesInControllerWithSeoAddition ?? throw new Exception("method getController in AnimeControllerTest");

//        return new AnimeController(
//            allServices.CrudService,
//            allServices.SeoAdditionService,
//            _mapper,
//            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager, allServicesInController.RoleManager),
//            alternativeServices.GetRequiredService<KindService>(),
//            alternativeServices.GetRequiredService<StatusService>(),
//            alternativeServices.GetRequiredService<PeriodService>(),
//            alternativeServices.GetRequiredService<RestrictedRatingService>(),
//            alternativeServices.GetRequiredService<SourceService>(),
//            alternativeServices.GetRequiredService<FileHelper>()
//        );
//    }

//    protected override void MutationBeforeDtoCreation(CreateAnimeDto createDto, AllServicesInController allServicesInController, IServiceProvider alternativeServices)
//    {
//        var kind = new Kind()
//        {
//            SeoAddition = GetSample(),
//            Slug = Faker.Lorem.GetFirstWord(),
//            Hint = Faker.Lorem.GetFirstWord(),
//        };

//        var status = new Status()
//        {
//            SeoAddition = GetSample(),
//            Name = Faker.Lorem.GetFirstWord(),
//        };

//        var period = new Period()
//        {
//            SeoAddition = GetSample(),
//            Name = Faker.Lorem.GetFirstWord(),
//        };

//        var restrictedRating = new RestrictedRating()
//        {
//            SeoAddition = GetSample(),
//            Name = Faker.Lorem.GetFirstWord(),
//            Icon = Faker.Lorem.GetFirstWord(),
//            Hint = Faker.Lorem.GetFirstWord(),
//            Value = Faker.RandomNumber.Next(),
//        };

//        var source = new Source()
//        {
//            SeoAddition = GetSample(),
//            Name = Faker.Lorem.GetFirstWord(),
//        };

//        var kindService = alternativeServices.GetRequiredService<KindService>();
//        var statusService = alternativeServices.GetRequiredService<StatusService>();
//        var periodService = alternativeServices.GetRequiredService<PeriodService>();
//        var restrictedRatingService = alternativeServices.GetRequiredService<RestrictedRatingService>();
//        var sourceService = alternativeServices.GetRequiredService<SourceService>();

//        kindService.CreateAsync(kind, CancellationToken).Wait();
//        statusService.CreateAsync(status, CancellationToken).Wait();
//        periodService.CreateAsync(period, CancellationToken).Wait();
//        restrictedRatingService.CreateAsync(restrictedRating, CancellationToken).Wait();
//        sourceService.CreateAsync(source, CancellationToken).Wait();

//        createDto.KindId = kind.Id;
//        createDto.StatusId = status.Id;
//        createDto.PeriodId = period.Id;
//        createDto.RestrictedRatingId = restrictedRating.Id;
//        createDto.SourceId = source.Id;
//    }

//    protected override void MutationBeforeDtoUpdate(UpdateAnimeDto updateDto, AllServicesInController allServicesInController, IServiceProvider alternativeServices)
//    {
//        var kind = new Kind()
//        {
//            SeoAddition = GetSample(),
//            Slug = Faker.Lorem.GetFirstWord(),
//            Hint = Faker.Lorem.GetFirstWord(),
//        };

//        var status = new Status()
//        {
//            SeoAddition = GetSample(),
//            Name = Faker.Lorem.GetFirstWord(),
//        };

//        var period = new Period()
//        {
//            SeoAddition = GetSample(),
//            Name = Faker.Lorem.GetFirstWord(),
//        };

//        var restrictedRating = new RestrictedRating()
//        {
//            SeoAddition = GetSample(),
//            Name = Faker.Lorem.GetFirstWord(),
//            Icon = Faker.Lorem.GetFirstWord(),
//            Hint = Faker.Lorem.GetFirstWord(),
//            Value = Faker.RandomNumber.Next(),
//        };

//        var source = new Source()
//        {
//            SeoAddition = GetSample(),
//            Name = Faker.Lorem.GetFirstWord(),
//        };

//        var kindService = alternativeServices.GetRequiredService<KindService>();
//        var statusService = alternativeServices.GetRequiredService<StatusService>();
//        var periodService = alternativeServices.GetRequiredService<PeriodService>();
//        var restrictedRatingService = alternativeServices.GetRequiredService<RestrictedRatingService>();
//        var sourceService = alternativeServices.GetRequiredService<SourceService>();

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

//    protected override CreateAnimeDto GetCreateDtoSample() => new()
//    {
//        SeoAddition = GetSeoAdditionCreateDtoSample(),
//        Name = Faker.Lorem.GetFirstWord(),
//        KindId = Guid.NewGuid(),
//        StatusId = Guid.NewGuid(),
//        PeriodId = Guid.NewGuid(),
//        RestrictedRatingId = Guid.NewGuid(),
//        SourceId = Guid.NewGuid(),
//        NativeName = Faker.Lorem.GetFirstWord(),
//        PosterColors = [Faker.RandomNumber.Next(), Faker.RandomNumber.Next(), Faker.RandomNumber.Next()],
//        AvgDuration = Faker.RandomNumber.Next(),
//        HowManyEpisodes = Faker.RandomNumber.Next(),
//        FirstAirDate = DateTime.Now,
//        LastAirDate = DateTime.Now,
//        ShikimoriScore = Faker.RandomNumber.Next(),
//        TmdbScore = Faker.RandomNumber.Next(),
//        ImdbScore = Faker.RandomNumber.Next(),
//        IsPublished = Faker.Boolean.Random(),
//        ImageName = Faker.Lorem.GetFirstWord(),
//        ShikimoriId = Faker.RandomNumber.Next(),
//        PublishedAt = DateTime.Now,
//        RomajiName = Faker.Lorem.GetFirstWord(),
//        TmdbId = Faker.RandomNumber.Next(),
//        UpdatedAt = DateTime.Now,
//        CreatedAt = DateTime.Now,
//        PosterImage
//    };

//    protected override GetAnimeDto GetGetDtoSample() => new()
//    {
//        SeoAddition = GetSeoAdditionGetDtoSample(),
//        Name = Faker.Lorem.GetFirstWord(),
//        KindId = Guid.NewGuid(),
//        StatusId = Guid.NewGuid(),
//        PeriodId = Guid.NewGuid(),
//        RestrictedRatingId = Guid.NewGuid(),
//        SourceId = Guid.NewGuid(),
//        NativeName = Faker.Lorem.GetFirstWord(),
//        PosterPath = Faker.Lorem.GetFirstWord(),
//        PosterColors = [Faker.RandomNumber.Next(), Faker.RandomNumber.Next(), Faker.RandomNumber.Next()],
//        AvgDuration = Faker.RandomNumber.Next(),
//        HowManyEpisodes = Faker.RandomNumber.Next(),
//        FirstAirDate = DateTime.Now,
//        LastAirDate = DateTime.Now,
//        ShikimoriScore = Faker.RandomNumber.Next(),
//        TmdbScore = Faker.RandomNumber.Next(),
//        ImdbScore = Faker.RandomNumber.Next(),
//        IsPublished = Faker.Boolean.Random(),
//        ImageName = Faker.Lorem.GetFirstWord(),
//        ShikimoriId = Faker.RandomNumber.Next(),
//        PublishedAt = DateTime.Now,
//        RomajiName = Faker.Lorem.GetFirstWord(),
//        TmdbId = Faker.RandomNumber.Next(),
//        UpdatedAt = DateTime.Now,
//        CreatedAt = DateTime.Now,
//        Id = Guid.NewGuid(),
//    };

//    protected override Anime GetModelSample() => new()
//    {
//        SeoAddition = GetSample(),
//        Name = Faker.Lorem.GetFirstWord(),
//        Kind = new()
//        {
//            SeoAddition = GetSample(),
//            Slug = Faker.Lorem.GetFirstWord(),
//            Hint = Faker.Lorem.GetFirstWord(),
//            Id = Guid.NewGuid(),
//        },
//        Status = new()
//        {
//            SeoAddition = GetSample(),
//            Name = Faker.Lorem.GetFirstWord(),
//            Id = Guid.NewGuid(),
//        },
//        Period = new()
//        {
//            SeoAddition = GetSample(),
//            Name = Faker.Lorem.GetFirstWord(),
//            Id = Guid.NewGuid(),
//        },
//        RestrictedRating = new()
//        {
//            SeoAddition = GetSample(),
//            Name = Faker.Lorem.GetFirstWord(),
//            Icon = Faker.Lorem.GetFirstWord(),
//            Hint = Faker.Lorem.GetFirstWord(),
//            Value = Faker.RandomNumber.Next(),
//            Id = Guid.NewGuid(),
//        },
//        Source = new()
//        {
//            SeoAddition = GetSample(),
//            Name = Faker.Lorem.GetFirstWord(),
//            Id = Guid.NewGuid(),
//        },
//        UpdatedAt = DateTime.Now,
//        CreatedAt = DateTime.Now,
//        NativeName = Faker.Lorem.GetFirstWord(),
//        PosterPath = Faker.Lorem.GetFirstWord(),
//        PosterColors = [Faker.RandomNumber.Next(), Faker.RandomNumber.Next(), Faker.RandomNumber.Next()],
//        AvgDuration = Faker.RandomNumber.Next(),
//        HowManyEpisodes = Faker.RandomNumber.Next(),
//        FirstAirDate = DateTime.Now,
//        LastAirDate = DateTime.Now,
//        ShikimoriScore = Faker.RandomNumber.Next(),
//        TmdbScore = Faker.RandomNumber.Next(),
//        ImdbScore = Faker.RandomNumber.Next(),
//        IsPublished = Faker.Boolean.Random(),
//        ImageName = Faker.Lorem.GetFirstWord(),
//        ShikimoriId = Faker.RandomNumber.Next(),
//        PublishedAt = DateTime.Now,
//        RomajiName = Faker.Lorem.GetFirstWord(),
//        TmdbId = Faker.RandomNumber.Next(),
//        Id = Guid.NewGuid(),
//    };

//    protected override UpdateAnimeDto GetUpdateDtoSample() => new()
//    {
//        SeoAddition = GetSeoAddtionUpdateDtoSample(),
//        Name = Faker.Lorem.GetFirstWord(),
//        KindId = Guid.NewGuid(),
//        StatusId = Guid.NewGuid(),
//        PeriodId = Guid.NewGuid(),
//        RestrictedRatingId = Guid.NewGuid(),
//        SourceId = Guid.NewGuid(),
//        NativeName = Faker.Lorem.GetFirstWord(),
//        PosterPath = Faker.Lorem.GetFirstWord(),
//        PosterColors = [Faker.RandomNumber.Next(), Faker.RandomNumber.Next(), Faker.RandomNumber.Next()],
//        AvgDuration = Faker.RandomNumber.Next(),
//        HowManyEpisodes = Faker.RandomNumber.Next(),
//        FirstAirDate = DateTime.Now,
//        LastAirDate = DateTime.Now,
//        ShikimoriScore = Faker.RandomNumber.Next(),
//        TmdbScore = Faker.RandomNumber.Next(),
//        ImdbScore = Faker.RandomNumber.Next(),
//        IsPublished = Faker.Boolean.Random(),
//        ImageName = Faker.Lorem.GetFirstWord(),
//        ShikimoriId = Faker.RandomNumber.Next(),
//        PublishedAt = DateTime.Now,
//        RomajiName = Faker.Lorem.GetFirstWord(),
//        TmdbId = Faker.RandomNumber.Next(),
//        UpdatedAt = DateTime.Now,
//        CreatedAt = DateTime.Now,
//        Id = Guid.NewGuid(),
//    };
//}