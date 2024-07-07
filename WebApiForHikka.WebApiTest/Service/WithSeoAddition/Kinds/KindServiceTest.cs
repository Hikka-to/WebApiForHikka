using WebApiForHikka.Application.Kinds;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithSeoAddition.Kinds;

public class KindServiceTest : SharedServiceTestWithSeoAddition<Kind, KindService>
{

    protected override Kind GetSample() => GetKindModels.GetSample();
    protected override Kind GetSampleForUpdate() => GetKindModels.GetSampleForUpdate();
    protected override KindService GetService(HikkaDbContext hikkaDbContext)
    {
        KindRepository repository = new(hikkaDbContext);

        return new KindService(repository);
    }
}