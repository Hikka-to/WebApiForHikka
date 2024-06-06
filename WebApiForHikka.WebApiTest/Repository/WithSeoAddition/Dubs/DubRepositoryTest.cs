using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Dubs;

public class DubRepositoryTest : SharedRepositoryTestWithSeoAddition<
    Dub,
    DubRepository
    >

{
    protected override DubRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new DubRepository(hikkaDbContext);
    }

    protected override Dub GetSample()
    {
        return new Dub()
        {
            Icon = "Icon",
            Name = "Name",
            SeoAddition = GetSeoAdditionSample(),
        };
    }

    protected override Dub GetSampleForUpdate()
    {
        return new Dub()
        {
            Icon = "Icon1",
            Name = "Name1",
            SeoAddition = GetSeoAdditionSampleUpdate(),
        };
    }
}
