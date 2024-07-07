using WebApiForHikka.Application.WithoutSeoAddition.AnimeVideoKinds;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithoutSeoAddition.AnimeVideoKinds;

public class AnimeVideoKindServiceTest : SharedServiceTest<AnimeVideoKind, AnimeVideoKindService>
{
    public AnimeVideoKind Sample => GetSample();

    public AnimeVideoKind SampleForUpdate => GetSampleForUpdate();

    protected override AnimeVideoKind GetSample() => GetAnimeVideoKindModels.GetSample();
    protected override AnimeVideoKind GetSampleForUpdate() => GetAnimeVideoKindModels.GetSampleForUpdate();
    protected override AnimeVideoKindService GetService(HikkaDbContext hikkaDbContext) => new(new AnimeVideoKindRepository(hikkaDbContext));
}
