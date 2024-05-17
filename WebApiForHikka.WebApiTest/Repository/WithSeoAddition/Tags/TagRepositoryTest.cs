
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
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
        return new Tag()
        {
            Alises = "test",
            EngName = "test",
            IsGenre = true,
            Name = "test",
            SeoAddition = GetSeoAdditionSample(),
        };
    }

    protected override Tag GetSampleForUpdate()
    {
        return new Tag()
        {
            Alises = "test1",
            EngName = "test1",
            IsGenre = false,
            Name = "test1",
            SeoAddition = GetSeoAdditionSampleUpdate(),
            ParentTag = new Tag()
            {
                Alises = "test",
                EngName = "test",
                IsGenre = true,
                Name = "test",
                SeoAddition = GetSeoAdditionSample(),
            },
        };
    }
}
