using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.ManyToMany;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.CountryAnimes;

public interface ICountryAnimeRelationRepository : IRelationCrudRepository<CountryAnime, Country, Anime>;