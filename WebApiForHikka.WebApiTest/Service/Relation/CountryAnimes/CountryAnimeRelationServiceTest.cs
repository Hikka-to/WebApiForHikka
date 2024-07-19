using WebApiForHikka.Application.Relation.CountryAnimes;
using WebApiForHikka.Domain.Models.Relation;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.Test.Shared.Models.Relation;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.Relation.CountryAnimes;

public class CountryAnimeRelationServiceTest : SharedRelationServiceTest<
    CountryAnime,
    CountryAnimeRelationService,
    Country,
    Anime
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

    protected override CountryAnimeRelationService GetService(HikkaDbContext hikkaDbContext)
    {
        return new CountryAnimeRelationService(new CountryAnimeRelationRepository(hikkaDbContext));
    }
}