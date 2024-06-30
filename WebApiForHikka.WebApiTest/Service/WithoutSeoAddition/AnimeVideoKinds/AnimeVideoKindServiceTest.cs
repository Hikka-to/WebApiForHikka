using WebApiForHikka.Application.WithoutSeoAddition.AnimeVideoKinds;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithoutSeoAddition.AnimeVideoKinds;

public class AnimeVideoKindServiceTest : SharedServiceTest<AnimeVideoKind, AnimeVideoKindService>
{
    protected override AnimeVideoKind GetSample() => new()
    {
        Name = "Name",
    };

    protected override AnimeVideoKind GetSampleForUpdate() => new()
    {
        Name = "Name1",
    };

    protected override AnimeVideoKindService GetService(HikkaDbContext hikkaDbContext) => new(new AnimeVideoKindRepository(hikkaDbContext));
}
