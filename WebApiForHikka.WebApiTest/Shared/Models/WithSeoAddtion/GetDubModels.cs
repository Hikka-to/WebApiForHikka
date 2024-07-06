using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

public static class GetDubModels
{
    public static Dub GetSample()
    {
        return new Dub()
        {
            Icon = "Icon",
            Name = "Name",
            SeoAddition = GetSeoAdditionModels.GetSample(),
        };
    }

    public static Dub GetSampleForUpdate()
    {
        return new Dub()
        {
            Icon = "Icon1",
            Name = "Name1",
            SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
        };
    }
}
