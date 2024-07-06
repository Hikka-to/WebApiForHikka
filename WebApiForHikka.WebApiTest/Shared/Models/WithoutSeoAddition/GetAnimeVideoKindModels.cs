using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

public class GetAnimeVideoKindModels
{
    public static AnimeVideoKind GetSample() => new()
    {
        Name = "Name",
    };

    public static AnimeVideoKind GetSampleForUpdate() => new()
    {
        Name = "Name1",
    };

}
