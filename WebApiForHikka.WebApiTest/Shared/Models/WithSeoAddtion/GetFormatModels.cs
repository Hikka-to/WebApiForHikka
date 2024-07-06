using WebApiForHikka.Domain.Models;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

public static class GetFormatModels
{
    public static Format GetSample() => new()
    {
        Name = "test",
        SeoAddition = GetSeoAdditionModels.GetSample(),
    };

    public static Format GetSampleForUpdate() => new()
    {
        Name = "test1",
        SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
    };
}
