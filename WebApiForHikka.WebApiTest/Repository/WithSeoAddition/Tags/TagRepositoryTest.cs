using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.SharedModels.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Tags;

public class TagRepositoryTest : SharedRepositoryTestWithSeoAddition<
    Tag,
    TagRepository
>
{
    protected override TagRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new TagRepository(hikkaDbContext);
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