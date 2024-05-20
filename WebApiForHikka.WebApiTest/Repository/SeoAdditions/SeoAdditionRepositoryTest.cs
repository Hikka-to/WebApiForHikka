using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.SeoAdditions;
public class SeoAdditionRepositoryTest : SharedRepositoryTest<SeoAddition, SeoAdditionRepository>
{
    protected override SeoAdditionRepository GetRepository(HikkaDbContext hikkaDbContext) =>
        new(hikkaDbContext);

    protected override SeoAddition GetSample() => new()
    {
        Description = "test",
        Slug = "test",
        Title = "test",
        Image = "test",
        ImageAlt = "test",
        SocialImage = "test",
        SocialImageAlt = "test",
        SocialTitle = "test",
        SocialType = "test",
    };

    protected override SeoAddition GetSampleForUpdate() => new()
    {
        Description = "test1",
        Slug = "test1",
        Title = "test1",
        Image = "test1",
        ImageAlt = "test1",
        SocialImage = "test1",
        SocialImageAlt = "test1",
        SocialTitle = "test1",
        SocialType = "test1",
    };
}