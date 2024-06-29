using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithoutSeoAddition.AnimeVideoKinds;

public class AnimeVideoKindRepositoryTest : SharedRepositoryTest<AnimeVideoKind, AnimeVideoKindRepository>
{
    protected override AnimeVideoKindRepository GetRepository(HikkaDbContext hikkaDbContext) => new(hikkaDbContext);

    protected override AnimeVideoKind GetSample() => new()
    {
        Name = "Name",
    };

    protected override AnimeVideoKind GetSampleForUpdate() => new()
    {
        Name = "Name1",
    };
}
