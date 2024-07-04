using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.ManyToMany;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.CountryAnimes;

public interface ICountryAnimeRelationService : IRelationCrudService<CountryAnime, Country, Anime>;
