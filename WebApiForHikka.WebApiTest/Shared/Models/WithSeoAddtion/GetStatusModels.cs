using WebApiForHikka.Domain.Models;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

public class GetStatusModels
{
    public static Status GetSample() => new()
    {
        Name = "Test",
        SeoAddition = GetSeoAdditionModels.GetSample(),
    };

    public static Status GetSampleForUpdate() => new()
    {
        Name = "test1",
        SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
    };
}
