using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

namespace WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

public class GetAnimeBackdropModels
{
    public static AnimeBackdrop GetSample() => new()
    {
        Anime = GetAnimeModels.GetSample(),
        Path = "Test",
        Width = 1,
        Height = 1,
        Colors = [1, 2, 3],
    };

    public static AnimeBackdrop GetSampleForUpdate() => new()
    {
        Anime = GetAnimeModels.GetSampleForUpdate(),
        Path = "Test1",
        Width = 2,
        Height = 2,
        Colors = [4, 5, 6],
    };
}
