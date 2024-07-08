using WebApiForHikka.Application.WithSeoAddition.Tags;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Tags;

public class TagServiceTest : SharedServiceTestWithSeoAddition<
    Tag,
    TagService
>
{
    protected override TagService GetService(HikkaDbContext hikkaDbContext)
    {
        TagRepository rep = new(hikkaDbContext);

        return new TagService(rep);
    }

    protected override Tag GetSample()
    {
        return GetTagModels.GetSample();
    }

    protected override Tag GetSampleForUpdate()
    {
        return GetTagModels.GetSampleForUpdate();
    }
}