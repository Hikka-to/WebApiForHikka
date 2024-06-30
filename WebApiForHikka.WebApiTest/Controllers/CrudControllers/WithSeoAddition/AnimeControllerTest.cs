﻿using Microsoft.Extensions.DependencyInjection;
using WebApiForHikka.Application.Kinds;
using WebApiForHikka.Application.Periods;
using WebApiForHikka.Application.RestrictedRatings;
using WebApiForHikka.Application.SeoAdditions;
using WebApiForHikka.Application.Sources;
using WebApiForHikka.Application.Statuses;
using WebApiForHikka.Application.WithSeoAddition.Animes;
using WebApiForHikka.Constants.Models.Animes;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Animes;
using WebApiForHikka.Dtos.Shared;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Controllers.Shared;
using WebApiForHikka.WebApi.Controllers.ControllersWithSeoAddition;

namespace WebApiForHikka.Test.Controllers.CrudControllers.WithSeoAddition;

public class AnimeControllerTest : CrudControllerBaseWithSeoAddition<
    AnimeController,
    AnimeService,
    Anime,
    IAnimeRepository,
    UpdateAnimeDto,
    CreateAnimeDto,
    GetAnimeDto,
    ReturnPageDto<GetAnimeDto>,
    AnimeStringConstants
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
        alternativeServices.AddSingleton<KindService>();
        alternativeServices.AddSingleton<StatusService>();
        alternativeServices.AddSingleton<PeriodService>();
        alternativeServices.AddSingleton<RestrictedRatingService>();
        alternativeServices.AddSingleton<SourceService>();

        return new AllServicesInControllerWithSeoAddition(new AnimeService(animeRepository), new SeoAdditionService(seoAdditionRepository), userManager, roleManager);
    }

    protected override async Task<AnimeController> GetController(AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        AllServicesInControllerWithSeoAddition allServices = allServicesInController as AllServicesInControllerWithSeoAddition ?? throw new Exception("method getController in AnimeControllerTest");


        return new AnimeController(
            allServices.CrudService,
            allServices.SeoAdditionService,
            _mapper,
            await GetHttpContextAccessForAdminUser(allServicesInController.UserManager, allServicesInController.RoleManager),
            alternativeServices.GetRequiredService<KindService>(),
            alternativeServices.GetRequiredService<StatusService>(),
            alternativeServices.GetRequiredService<PeriodService>(),
            alternativeServices.GetRequiredService<RestrictedRatingService>(),
            alternativeServices.GetRequiredService<SourceService>()
            );
    }

    protected override void MutationBeforeDtoCreation(CreateAnimeDto createDto, AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        var kind = new Kind()
        {
            SeoAddition = GetSeoAdditionSample(),
            Slug = Faker.Lorem.GetFirstWord(),
            Hint = Faker.Lorem.GetFirstWord(),
        };

        var status = new Status()
        {
            SeoAddition = GetSeoAdditionSample(),
            Name = Faker.Lorem.GetFirstWord(),
        };

        var period = new Period()
        {
            SeoAddition = GetSeoAdditionSample(),
            Name = Faker.Lorem.GetFirstWord(),
        };

        var restrictedRating = new RestrictedRating()
        {
            SeoAddition = GetSeoAdditionSample(),
            Name = Faker.Lorem.GetFirstWord(),
            Icon = Faker.Lorem.GetFirstWord(),
            Hint = Faker.Lorem.GetFirstWord(),
            Value = Faker.RandomNumber.Next(),
        };

        var source = new Source()
        {
            SeoAddition = GetSeoAdditionSample(),
            Name = Faker.Lorem.GetFirstWord(),
        };

        var kindService = alternativeServices.GetRequiredService<KindService>();
        var statusService = alternativeServices.GetRequiredService<StatusService>();
        var periodService = alternativeServices.GetRequiredService<PeriodService>();
        var restrictedRatingService = alternativeServices.GetRequiredService<RestrictedRatingService>();
        var sourceService = alternativeServices.GetRequiredService<SourceService>();

        kindService.CreateAsync(kind, CancellationToken).Wait();
        statusService.CreateAsync(status, CancellationToken).Wait();
        periodService.CreateAsync(period, CancellationToken).Wait();
        restrictedRatingService.CreateAsync(restrictedRating, CancellationToken).Wait();
        sourceService.CreateAsync(source, CancellationToken).Wait();

        createDto.KindId = kind.Id;
        createDto.StatusId = status.Id;
        createDto.PeriodId = period.Id;
        createDto.RestrictedRatingId = restrictedRating.Id;
        createDto.SourceId = source.Id;
    }

    protected override void MutationBeforeDtoUpdate(UpdateAnimeDto updateDto, AllServicesInController allServicesInController, IServiceProvider alternativeServices)
    {
        var kind = new Kind()
        {
            SeoAddition = GetSeoAdditionSample(),
            Slug = Faker.Lorem.GetFirstWord(),
            Hint = Faker.Lorem.GetFirstWord(),
        };

        var status = new Status()
        {
            SeoAddition = GetSeoAdditionSample(),
            Name = Faker.Lorem.GetFirstWord(),
        };

        var period = new Period()
        {
            SeoAddition = GetSeoAdditionSample(),
            Name = Faker.Lorem.GetFirstWord(),
        };

        var restrictedRating = new RestrictedRating()
        {
            SeoAddition = GetSeoAdditionSample(),
            Name = Faker.Lorem.GetFirstWord(),
            Icon = Faker.Lorem.GetFirstWord(),
            Hint = Faker.Lorem.GetFirstWord(),
            Value = Faker.RandomNumber.Next(),
        };

        var source = new Source()
        {
            SeoAddition = GetSeoAdditionSample(),
            Name = Faker.Lorem.GetFirstWord(),
        };

        var kindService = alternativeServices.GetRequiredService<KindService>();
        var statusService = alternativeServices.GetRequiredService<StatusService>();
        var periodService = alternativeServices.GetRequiredService<PeriodService>();
        var restrictedRatingService = alternativeServices.GetRequiredService<RestrictedRatingService>();
        var sourceService = alternativeServices.GetRequiredService<SourceService>();

        kindService.CreateAsync(kind, CancellationToken).Wait();
        statusService.CreateAsync(status, CancellationToken).Wait();
        periodService.CreateAsync(period, CancellationToken).Wait();
        restrictedRatingService.CreateAsync(restrictedRating, CancellationToken).Wait();
        sourceService.CreateAsync(source, CancellationToken).Wait();

        updateDto.KindId = kind.Id;
        updateDto.StatusId = status.Id;
        updateDto.PeriodId = period.Id;
        updateDto.RestrictedRatingId = restrictedRating.Id;
        updateDto.SourceId = source.Id;
    }

    protected override CreateAnimeDto GetCreateDtoSample() => new()
    {
        SeoAddition = GetSeoAdditionCreateDtoSample(),
        Name = Faker.Lorem.GetFirstWord(),
        KindId = Guid.NewGuid(),
        StatusId = Guid.NewGuid(),
        PeriodId = Guid.NewGuid(),
        RestrictedRatingId = Guid.NewGuid(),
        SourceId = Guid.NewGuid(),
        NativeName = Faker.Lorem.GetFirstWord(),
        PosterPath = Faker.Lorem.GetFirstWord(),
        PosterColors = [Faker.RandomNumber.Next(), Faker.RandomNumber.Next(), Faker.RandomNumber.Next()],
        AvgDuration = Faker.RandomNumber.Next(),
        HowManyEpisodes = Faker.RandomNumber.Next(),
        FirstAirDate = DateTime.Now,
        LastAirDate = DateTime.Now,
        ShikimoriScore = Faker.RandomNumber.Next(),
        TmdbScore = Faker.RandomNumber.Next(),
        ImdbScore = Faker.RandomNumber.Next(),
        IsPublished = Faker.Boolean.Random(),
        ImageName = Faker.Lorem.GetFirstWord(),
        ShikimoriId = Faker.RandomNumber.Next(),
        PublishedAt = DateTime.Now,
        RomajiName = Faker.Lorem.GetFirstWord(),
        TmdbId = Faker.RandomNumber.Next(),
        UpdatedAt = DateTime.Now,
        CreatedAt = DateTime.Now,
    };

    protected override GetAnimeDto GetGetDtoSample() => new()
    {
        SeoAddition = GetSeoAdditionGetDtoSample(),
        Name = Faker.Lorem.GetFirstWord(),
        KindId = Guid.NewGuid(),
        StatusId = Guid.NewGuid(),
        PeriodId = Guid.NewGuid(),
        RestrictedRatingId = Guid.NewGuid(),
        SourceId = Guid.NewGuid(),
        NativeName = Faker.Lorem.GetFirstWord(),
        PosterPath = Faker.Lorem.GetFirstWord(),
        PosterColors = [Faker.RandomNumber.Next(), Faker.RandomNumber.Next(), Faker.RandomNumber.Next()],
        AvgDuration = Faker.RandomNumber.Next(),
        HowManyEpisodes = Faker.RandomNumber.Next(),
        FirstAirDate = DateTime.Now,
        LastAirDate = DateTime.Now,
        ShikimoriScore = Faker.RandomNumber.Next(),
        TmdbScore = Faker.RandomNumber.Next(),
        ImdbScore = Faker.RandomNumber.Next(),
        IsPublished = Faker.Boolean.Random(),
        ImageName = Faker.Lorem.GetFirstWord(),
        ShikimoriId = Faker.RandomNumber.Next(),
        PublishedAt = DateTime.Now,
        RomajiName = Faker.Lorem.GetFirstWord(),
        TmdbId = Faker.RandomNumber.Next(),
        UpdatedAt = DateTime.Now,
        CreatedAt = DateTime.Now,
        Id = Guid.NewGuid(),
    };

    protected override Anime GetModelSample() => new()
    {
        SeoAddition = GetSeoAdditionSample(),
        Name = Faker.Lorem.GetFirstWord(),
        Kind = new()
        {
            SeoAddition = GetSeoAdditionSample(),
            Slug = Faker.Lorem.GetFirstWord(),
            Hint = Faker.Lorem.GetFirstWord(),
            Id = Guid.NewGuid(),
        },
        Status = new()
        {
            SeoAddition = GetSeoAdditionSample(),
            Name = Faker.Lorem.GetFirstWord(),
            Id = Guid.NewGuid(),
        },
        Period = new()
        {
            SeoAddition = GetSeoAdditionSample(),
            Name = Faker.Lorem.GetFirstWord(),
            Id = Guid.NewGuid(),
        },
        RestrictedRating = new()
        {
            SeoAddition = GetSeoAdditionSample(),
            Name = Faker.Lorem.GetFirstWord(),
            Icon = Faker.Lorem.GetFirstWord(),
            Hint = Faker.Lorem.GetFirstWord(),
            Value = Faker.RandomNumber.Next(),
            Id = Guid.NewGuid(),
        },
        Source = new()
        {
            SeoAddition = GetSeoAdditionSample(),
            Name = Faker.Lorem.GetFirstWord(),
            Id = Guid.NewGuid(),
        },
        UpdatedAt = DateTime.Now,
        CreatedAt = DateTime.Now,
        NativeName = Faker.Lorem.GetFirstWord(),
        PosterPath = Faker.Lorem.GetFirstWord(),
        PosterColors = [Faker.RandomNumber.Next(), Faker.RandomNumber.Next(), Faker.RandomNumber.Next()],
        AvgDuration = Faker.RandomNumber.Next(),
        HowManyEpisodes = Faker.RandomNumber.Next(),
        FirstAirDate = DateTime.Now,
        LastAirDate = DateTime.Now,
        ShikimoriScore = Faker.RandomNumber.Next(),
        TmdbScore = Faker.RandomNumber.Next(),
        ImdbScore = Faker.RandomNumber.Next(),
        IsPublished = Faker.Boolean.Random(),
        ImageName = Faker.Lorem.GetFirstWord(),
        ShikimoriId = Faker.RandomNumber.Next(),
        PublishedAt = DateTime.Now,
        RomajiName = Faker.Lorem.GetFirstWord(),
        TmdbId = Faker.RandomNumber.Next(),
        Id = Guid.NewGuid(),
    };


    protected override UpdateAnimeDto GetUpdateDtoSample() => new()
    {
        SeoAddition = GetSeoAddtionUpdateDtoSample(),
        Name = Faker.Lorem.GetFirstWord(),
        KindId = Guid.NewGuid(),
        StatusId = Guid.NewGuid(),
        PeriodId = Guid.NewGuid(),
        RestrictedRatingId = Guid.NewGuid(),
        SourceId = Guid.NewGuid(),
        NativeName = Faker.Lorem.GetFirstWord(),
        PosterPath = Faker.Lorem.GetFirstWord(),
        PosterColors = [Faker.RandomNumber.Next(), Faker.RandomNumber.Next(), Faker.RandomNumber.Next()],
        AvgDuration = Faker.RandomNumber.Next(),
        HowManyEpisodes = Faker.RandomNumber.Next(),
        FirstAirDate = DateTime.Now,
        LastAirDate = DateTime.Now,
        ShikimoriScore = Faker.RandomNumber.Next(),
        TmdbScore = Faker.RandomNumber.Next(),
        ImdbScore = Faker.RandomNumber.Next(),
        IsPublished = Faker.Boolean.Random(),
        ImageName = Faker.Lorem.GetFirstWord(),
        ShikimoriId = Faker.RandomNumber.Next(),
        PublishedAt = DateTime.Now,
        RomajiName = Faker.Lorem.GetFirstWord(),
        TmdbId = Faker.RandomNumber.Next(),
        UpdatedAt = DateTime.Now,
        CreatedAt = DateTime.Now,
        Id = Guid.NewGuid(),
    };
}
