using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

public static class GetCountryModels
{
    public static Country GetSample()
    {
        return new Country()
        {
            Icon = "Icon",
            Name = "Name",
            SeoAddition = GetSeoAdditionModels.GetSample(),
        };
    }

    public static Country GetSampleForUpdate()
    {
        return new Country()
        {
            Icon = "Icon1",
            Name = "Name1",
            SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
        };
    }

}
