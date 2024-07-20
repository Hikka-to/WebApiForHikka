using WebApiForHikka.Application.Relation.TagAnimes;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.Relation.TagAnimes;

public class TagAnimeRelationServiceTest : SharedRelationServiceTest<
    TagAnime,
    TagAnimeRelationService,
    Tag,
    Anime
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

    protected override TagAnimeRelationService GetService(HikkaDbContext hikkaDbContext)
    {
        return new TagAnimeRelationService(new TagAnimeRelationRepository(hikkaDbContext));
    }
}