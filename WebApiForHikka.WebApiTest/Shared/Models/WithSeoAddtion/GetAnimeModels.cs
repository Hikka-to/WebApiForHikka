using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

public static class GetAnimeModels
{
    public static Tag GetTagWithName(string name) => new Tag
    {
        Name = name,
        Alises = [],
        EngName = name,
        IsGenre = true,
        SeoAddition = GetSeoAdditionModels.GetSample(),
    };

    public static Country GetCountryWithName(string name) => new Country
    {
        Icon = name,
        Name = name,
        SeoAddition = GetSeoAdditionModels.GetSample(),
    };
    public static Dub GetDubWithName(string name) => new Dub
    {
        Icon = name,
        Name = name,
        SeoAddition = GetSeoAdditionModels.GetSample(),

    };


    public static  Anime GetSample() => new()
    {
        Name = "Test",
        SeoAddition = GetSeoAdditionModels.GetSample(),
        Kind = new()
        {
            Hint = "Test",
            Slug = "Test",
            SeoAddition = GetSeoAdditionModels.GetSample(),
        },
        Source = new()
        {
            Name = "Test",
            SeoAddition = GetSeoAdditionModels.GetSample(),
        },
        Status = new()
        {
            Name = "Test",
            SeoAddition = GetSeoAdditionModels.GetSample(),
        },
        Period = new()
        {
            Name = "Test",
            SeoAddition = GetSeoAdditionModels.GetSample(),
        },
        RestrictedRating = new()
        {
            Value = 1,
            Name = "Test",
            Icon = "Test",
            Hint = "Test",
            SeoAddition = GetSeoAdditionModels.GetSample(),
        },
        Countries = [
            GetCountryWithName("name")
            ],
        Dubs = [
            GetDubWithName("name")
            ],
        Tags = [
            GetTagWithName("test")
            ],
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

    public static  Anime GetSampleForUpdate() => new()
    {
        Name = "Test1",
        SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
        Kind = new()
        {
            Hint = "Test1",
            Slug = "Test1",
            SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
        },
        Source = new()
        {
            Name = "Test1",
            SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
        },
        Status = new()
        {
            Name = "Test1",
            SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
        },
        Period = new()
        {
            Name = "Test1",
            SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
        },
        RestrictedRating = new()
        {
            Value = 2,
            Name = "Test1",
            Icon = "Test1",
            Hint = "Test1",
            SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
        },
        Countries = [
            GetCountryWithName("name1")
            ],
        Dubs = [
            GetDubWithName("name1")
            ],
        Tags = [
            GetTagWithName("test1")
            ],
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
