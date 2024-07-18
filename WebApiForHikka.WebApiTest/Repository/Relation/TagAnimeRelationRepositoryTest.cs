using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.Test.Shared.Models.Relation;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.Relation;

public class TagAnimeRelationRepositoryTest : SharedRelationRepositoryTest<
    TagAnime, Tag, Anime,
    TagAnimeRelationRepository
>
{
    protected override TagAnime GetSample()
    {
        return GetTagAnimeModels.GetSample();
    }

    protected override TagAnime GetSampleForUpdate()
    {
        return GetTagAnimeModels.GetSampleForUpdate();
    }

    protected override TagAnimeRelationRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new TagAnimeRelationRepository(hikkaDbContext);
    }
}