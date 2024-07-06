using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithSeoAddition.Kinds;

public class KindRepositoryTest : SharedRepositoryTestWithSeoAddition<
    Kind,
    KindRepository
    >
{
    protected override KindRepository GetRepository(HikkaDbContext hikkaDbContext) =>
        new(hikkaDbContext);

    protected override Kind GetSample() => GetKindModels.GetSample();
    protected override Kind GetSampleForUpdate() => GetKindModels.GetSampleForUpdate();

}