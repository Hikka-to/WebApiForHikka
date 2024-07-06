using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

public class GetStudioModels
{
    public static Studio GetSample() => new()
    {
        Name = "Test",
        Logo = "Test",
        SeoAddition = GetSeoAdditionModels.GetSample(),
    };

    public static Studio GetSampleForUpdate() => new()
    {
        Name = "test1",
        Logo = "logo1",
        SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
    };
}
