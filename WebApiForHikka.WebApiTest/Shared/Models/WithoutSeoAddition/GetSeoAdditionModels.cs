using WebApiForHikka.Domain.Models;
using WebApiForHikka.Test.Shared.Models.Shared;

namespace WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

public static class GetSeoAdditionModels 
{
    public static SeoAddition GetSample()
    {
        return new SeoAddition()
        {
            Description = "Test",
            Slug = "Test",
            Title = "Test",
            Image = "Test",
            ImageAlt = "Test",
            SocialImage = "Test",
            SocialImageAlt = "Test",
        };
    }
    public static SeoAddition GetSampleForUpdate() => new()
    {
        Description = "Test1",
        Slug = "Test1",
        Title = "Test1",
        Image = "Test1",
        ImageAlt = "Test1",
        SocialImage = "Test1",
        SocialImageAlt = "Test1",
    };
}
