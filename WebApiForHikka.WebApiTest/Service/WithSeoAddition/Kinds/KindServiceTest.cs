using WebApiForHikka.Application.Kinds;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Kinds;

public class KindServiceTest : SharedServiceTestWithSeoAddition<Kind, KindService>
{

    protected override Kind GetSample() => new()
    {
        Hint = "Test",
        Slug = "Test",
        SeoAddition = GetSeoAdditionSample(),
    };

    protected override Kind GetSampleForUpdate() => new()
    {
        Hint = "Test1",
        Slug = "Test1",
        SeoAddition = GetSeoAdditionSampleUpdate(),
    };

    protected override KindService GetService(HikkaDbContext hikkaDbContext)
    {
        KindRepository repository = new(hikkaDbContext);

        return new KindService(repository);
    }
}