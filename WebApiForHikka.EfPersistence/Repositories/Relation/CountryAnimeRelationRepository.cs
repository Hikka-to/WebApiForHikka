using WebApiForHikka.Application.Relation.CountryAnimes;
using WebApiForHikka.Domain.Models.ManyToMany;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.Relation;

public class CountryAnimeRelationRepository(HikkaDbContext dbContext)
    : CrudRelationRepository<CountryAnime, Country, Anime>(dbContext), ICountryAnimeRelationRepository;