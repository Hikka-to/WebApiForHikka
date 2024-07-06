using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithoutSeoAddition.AnimeVideoKinds;

public class AnimeVideoKindRepositoryTest : SharedRepositoryTest<AnimeVideoKind, AnimeVideoKindRepository>
{
    public AnimeVideoKind Sample => GetSample();

    public AnimeVideoKind SampleForUpdate => GetSampleForUpdate();

    protected override AnimeVideoKindRepository GetRepository(HikkaDbContext hikkaDbContext) => new(hikkaDbContext);

    protected override AnimeVideoKind GetSample() => GetAnimeVideoKindModels.GetSample();

    protected override AnimeVideoKind GetSampleForUpdate() => GetAnimeVideoKindModels.GetSampleForUpdate();

}
