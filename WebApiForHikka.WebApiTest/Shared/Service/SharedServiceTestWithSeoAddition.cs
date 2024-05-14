using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Test.Shared.Service;

public abstract class SharedServiceTestWithSeoAddition<TModel, TService> : SharedServiceTest<TModel, TService> where TModel : ModelWithSeoAddition where TService : ICrudService<TModel>
{
    protected SeoAddition GetSeoAdditionSample()
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
    protected SeoAddition GetSeoAdditionSampleUpdate()
    {
        return new SeoAddition()
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
}
