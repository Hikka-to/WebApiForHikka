using WebApiForHikka.Domain.Models;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

public static class GetRestrictedRatingModels
{
    public static RestrictedRating GetSample() => new()
    {
        Name = "test",
        Hint = "test",
        Icon = "test",
        Value = 1,
        SeoAddition = GetSeoAdditionModels.GetSample(),
    };

    public static RestrictedRating GetSampleForUpdate() => new()
    {
        Name = "test1",
        Hint = "test1",
        Icon = "test1",
        Value = 2,
        SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
    };
}
