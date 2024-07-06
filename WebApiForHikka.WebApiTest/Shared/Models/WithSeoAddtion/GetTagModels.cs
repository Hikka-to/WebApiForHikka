using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

public static class GetTagModels
{
    public static Tag GetSample() => new()
    {
        Alises = ["test"],
        EngName = "test",
        IsGenre = true,
        Name = "test",
        SeoAddition = GetSeoAdditionModels.GetSample(),
    };

    public static Tag GetSampleForUpdate() => new()
    {
        Alises = ["test1"],
        EngName = "test1",
        IsGenre = false,
        Name = "test1",
        SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
        ParentTag = new()
        {
            Alises = ["test"],
            EngName = "test",
            IsGenre = true,
            Name = "test",
            SeoAddition = GetSeoAdditionModels.GetSample(),
        },
    };
}
