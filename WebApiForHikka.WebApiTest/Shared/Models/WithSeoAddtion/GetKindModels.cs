using WebApiForHikka.Domain.Models;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

public static class GetKindModels
{
    public static Kind GetSample() => new()
    {
        Hint = "test",
        Slug = "test",
        SeoAddition = GetSeoAdditionModels.GetSample(),
    };

    public static Kind GetSampleForUpdate() => new()
    {

        Hint = "test1",
        Slug = "test1",
        SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
    };
}
