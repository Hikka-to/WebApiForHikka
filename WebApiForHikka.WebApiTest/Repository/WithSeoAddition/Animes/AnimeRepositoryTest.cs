using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Animes;

public class AnimeRepositoryTest : SharedRepositoryTestWithSeoAddition<Anime, AnimeRepository>
{
    public Anime Anime => GetSample();

    public Anime AnimeForUpdate => GetSampleForUpdate();

    protected override AnimeRepository GetRepository(HikkaDbContext hikkaDbContext) => new(hikkaDbContext);


    protected override Anime GetSample() => new()
    {
        Name = "Test",
        SeoAddition = GetSeoAdditionSample(),
        Kind = new()
        {
            Hint = "Test",
            Slug = "Test",
            SeoAddition = GetSeoAdditionSample(),
        },
        Source = new()
        {
            Name = "Test",
            SeoAddition = GetSeoAdditionSample(),
        },
        Status = new()
        {
            Name = "Test",
            SeoAddition = GetSeoAdditionSample(),
        },
        Period = new()
        {
            Name = "Test",
            SeoAddition = GetSeoAdditionSample(),
        },
        RestrictedRating = new()
        {
            Value = 1,
            Name = "Test",
            Icon = "Test",
            Hint = "Test",
            SeoAddition = GetSeoAdditionSample(),
        },
        NativeName = "Test",
        ImageName = "Test",
        RomajiName = "Test",
        PosterPath = "Test",
        PosterColors = [1, 2, 3],
        AvgDuration = 1,
        HowManyEpisodes = 1,
        FirstAirDate = DateTime.Today,
        LastAirDate = DateTime.Today,
        TmdbId = 1,
        ShikimoriId = 1,
        ShikimoriScore = 1,
        TmdbScore = 1,
        ImdbScore = 1,
        IsPublished = true,
        PublishedAt = DateTime.Today,
        UpdatedAt = DateTime.Today,
        CreatedAt = DateTime.Today,
    };

    protected override Anime GetSampleForUpdate() => new()
    {
        Name = "Test1",
        SeoAddition = GetSeoAdditionSampleUpdate(),
        Kind = new()
        {
            Hint = "Test1",
            Slug = "Test1",
            SeoAddition = GetSeoAdditionSampleUpdate(),
        },
        Source = new()
        {
            Name = "Test1",
            SeoAddition = GetSeoAdditionSampleUpdate(),
        },
        Status = new()
        {
            Name = "Test1",
            SeoAddition = GetSeoAdditionSampleUpdate(),
        },
        Period = new()
        {
            Name = "Test1",
            SeoAddition = GetSeoAdditionSampleUpdate(),
        },
        RestrictedRating = new()
        {
            Value = 2,
            Name = "Test1",
            Icon = "Test1",
            Hint = "Test1",
            SeoAddition = GetSeoAdditionSampleUpdate(),
        },
        Tags = new List<Tag>() 
        {
        },
        NativeName = "Test1",
        ImageName = "Test1",
        RomajiName = "Test1",
        PosterPath = "Test1",
        PosterColors = [4, 5, 6],
        AvgDuration = 2,
        HowManyEpisodes = 2,
        FirstAirDate = DateTime.Now,
        LastAirDate = DateTime.Now,
        TmdbId = 2,
        ShikimoriId = 2,
        ShikimoriScore = 2,
        TmdbScore = 2,
        ImdbScore = 2,
        IsPublished = false,
        PublishedAt = DateTime.Now,
        UpdatedAt = DateTime.Now,
        CreatedAt = DateTime.Now,
    };
}