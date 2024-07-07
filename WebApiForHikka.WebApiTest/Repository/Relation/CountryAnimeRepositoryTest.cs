using WebApiForHikka.Domain.Models.ManyToMany;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.Relation;
using WebApiForHikka.EfPersistence.Repositories.WithSeoAddition;
using WebApiForHikka.Test.Shared.Models.WithSeoAddtion;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.Relation;

class CountryAnimeRepositoryTest : SharedRelationRepositoryTest<
    CountryAnime, Country, Anime,
    CountryAnimeRelationRepository, CountryRepository, AnimeRepository
    >
{
    protected override async Task<(Guid firstId, Guid secondId)> CreateFirstAndSecondModels((CountryRepository firstRepository, AnimeRepository secondRepository) repostiroeis)
    {
        var firstId = await repostiroeis.firstRepository.AddAsync(GetCountryModels.GetSample(), CancellationToken);

        var secondId = await repostiroeis.secondRepository.AddAsync(GetAnimeModels.GetSample(), CancellationToken);

        return (firstId, secondId);
    }

    protected override (CountryRepository firstRepository, AnimeRepository secondRepository) GetFirstAndSecondRepositories(HikkaDbContext hikkaDbContext)
    {
        return (
            new CountryRepository(hikkaDbContext),
            new AnimeRepository(hikkaDbContext)
            );

    }

    protected override Country GetFirstModelSample()
    {
        return GetCountryModels.GetSample(); 
    }

    protected override Anime GetSecondModelSample()
    {
        return GetAnimeModels.GetSample(); 
    }

    protected override CountryAnime GetRelationModel(Guid firstId, Guid secondId)
    {
        return new CountryAnime()
        {
            FirstId = firstId,
            SecondId = secondId,
        };
    }

    protected override CountryAnimeRelationRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new CountryAnimeRelationRepository(hikkaDbContext);
    }
}

