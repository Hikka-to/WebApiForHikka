﻿using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

public static class GetAnimeModels
{
    public static Tag GetTagWithName(string name)
    {
        return new Tag
        {
            Name = name,
            Alises = [],
            EngName = name,
            IsGenre = true,
            SeoAddition = GetSeoAdditionModels.GetSample()
        };
    }

    public static Country GetCountryWithName(string name)
    {
        return new Country
        {
            Icon = name,
            Name = name,
            SeoAddition = GetSeoAdditionModels.GetSample()
        };
    }

    public static Dub GetDubWithName(string name)
    {
        return new Dub
        {
            Icon = name,
            Name = name,
            SeoAddition = GetSeoAdditionModels.GetSample()
        };
    }


    public static Anime GetSample()
    {
        var sample = GetSampleWithoutManyToMany();

        sample.Countries =
        [
            GetCountryWithName("name")
        ];
        sample.Dubs =
        [
            GetDubWithName("name")
        ];
        sample.Tags =
        [
            GetTagWithName("test")
        ];

        return sample;
    }

    public static Anime GetSampleWithoutManyToMany()
    {
        return new Anime
        {
            Name = "Test",
            SeoAddition = GetSeoAdditionModels.GetSample(),
            Kind = new Kind
            {
                Hint = "Test",
                Slug = "Test",
                SeoAddition = GetSeoAdditionModels.GetSample()
            },
            Source = new Source
            {
                Name = "Test",
                SeoAddition = GetSeoAdditionModels.GetSample()
            },
            Status = new Status
            {
                Name = "Test",
                SeoAddition = GetSeoAdditionModels.GetSample()
            },
            Period = new Period
            {
                Name = "Test",
                SeoAddition = GetSeoAdditionModels.GetSample()
            },
            RestrictedRating = new RestrictedRating
            {
                Value = 1,
                Name = "Test",
                Icon = "Test",
                Hint = "Test",
                SeoAddition = GetSeoAdditionModels.GetSample()
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
            CreatedAt = DateTime.Today
        };
    }

    public static Anime GetSampleForUpdate()
    {
        var sample = GetSampleForUpdateWithoutManyToMany();

        sample.Countries =
        [
            GetCountryWithName("name1")
        ];
        sample.Dubs =
        [
            GetDubWithName("name1")
        ];
        sample.Tags =
        [
            GetTagWithName("test1")
        ];

        return sample;
    }

    public static Anime GetSampleForUpdateWithoutManyToMany()
    {
        return new Anime
        {
            Name = "Test1",
            SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
            Kind = new Kind
            {
                Hint = "Test1",
                Slug = "Test1",
                SeoAddition = GetSeoAdditionModels.GetSampleForUpdate()
            },
            Source = new Source
            {
                Name = "Test1",
                SeoAddition = GetSeoAdditionModels.GetSampleForUpdate()
            },
            Status = new Status
            {
                Name = "Test1",
                SeoAddition = GetSeoAdditionModels.GetSampleForUpdate()
            },
            Period = new Period
            {
                Name = "Test1",
                SeoAddition = GetSeoAdditionModels.GetSampleForUpdate()
            },
            RestrictedRating = new RestrictedRating
            {
                Value = 2,
                Name = "Test1",
                Icon = "Test1",
                Hint = "Test1",
                SeoAddition = GetSeoAdditionModels.GetSampleForUpdate()
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
            CreatedAt = DateTime.Now
        };
    }
}