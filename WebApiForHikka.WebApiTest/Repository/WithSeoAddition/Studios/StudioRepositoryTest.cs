using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Studios;

public class StudioRepositoryTest : SharedRepositoryTestWithSeoAddition<
    Studio,
    StudioRepository
    >
{
    protected override StudioRepository GetRepository(HikkaDbContext hikkaDbContext) => new(hikkaDbContext);

    protected override Studio GetSample() => new()
    {
        Name = "Test",
        Logo = "Test",
        SeoAddition = GetSeoAdditionSample(),
    };

    protected override Studio GetSampleForUpdate() => new()
    {
        Name = "test1",
        Logo = "logo1",
        SeoAddition = GetSeoAdditionSampleUpdate(),
    };


}
