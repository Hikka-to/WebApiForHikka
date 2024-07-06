using WebApiForHikka.Domain.Models;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

public static class GetSourceModels
{
    public static Source GetSample() => new()
    {
        Name = "test",
        SeoAddition = GetSeoAdditionModels.GetSample(),
    };

    public static Source GetSampleForUpdate() => new()
    {
        Name = "test1",
        SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
    };
}
