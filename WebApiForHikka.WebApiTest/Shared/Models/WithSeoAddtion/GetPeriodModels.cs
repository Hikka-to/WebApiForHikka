using WebApiForHikka.Domain.Models;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

public static class GetPeriodModels
{
    public static Period GetSample() => new()
    {
        Name = "test",
        SeoAddition = GetSeoAdditionModels.GetSample(),
    };

    public static Period GetSampleForUpdate() => new()
    {
        Name = "test1",
        SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
    };
}
