using WebApiForHikka.Application.Relation.DubAnimes;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.Test.Shared.Models.Relation;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.Relation.DubAnimes;

public class DubAnimeRelationServiceTest : SharedRelationServiceTest<
    DubAnime,
    DubAnimeRelationService,
    Dub,
    Anime
>
{
    protected override DubAnime GetSample()
    {
        return GetDubAnimeModels.GetSample();
    }

    protected override DubAnime GetSampleForUpdate()
    {
        return GetDubAnimeModels.GetSampleForUpdate();
    }

    protected override DubAnimeRelationService GetService(HikkaDbContext hikkaDbContext)
    {
        return new DubAnimeRelationService(new DubAnimeRelationRepository(hikkaDbContext));
    }
}