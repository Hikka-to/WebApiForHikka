using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.SharedModels.Models.Relation;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.Relation;

public class CountryAnimeRelationRepositoryTest : SharedRelationRepositoryTest<
    CountryAnime, Country, Anime,
    CountryAnimeRelationRepository
>
{
    protected override CountryAnime GetSample()
    {
        return GetCountryAnimeModels.GetSample();
    }

    protected override CountryAnime GetSampleForUpdate()
    {
        return GetCountryAnimeModels.GetSampleForUpdate();
    }

    protected override CountryAnimeRelationRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new CountryAnimeRelationRepository(hikkaDbContext);
    }
}