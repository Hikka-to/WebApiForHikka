﻿using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;

namespace WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

public class GetAnimeBackdropModels
{
    public static AnimeBackdrop GetSample()
    {
        return new AnimeBackdrop
        {
            Anime = GetAnimeModels.GetSample(),
            Path = "Test",
            Width = 1,
            Height = 1,
            Colors = [1, 2, 3]
        };
    }

    public static AnimeBackdrop GetSampleForUpdate()
    {
        return new AnimeBackdrop
        {
            Anime = GetAnimeModels.GetSampleForUpdate(),
            Path = "Test1",
            Width = 2,
            Height = 2,
            Colors = [4, 5, 6]
        };
    }
}