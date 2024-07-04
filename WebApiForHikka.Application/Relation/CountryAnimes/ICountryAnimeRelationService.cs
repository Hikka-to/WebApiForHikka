using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.ManyToMany;

namespace WebApiForHikka.Application.Relation.CountryAnimes;

public interface ICountryAnimeRelationService : IRelationCrudService<CountryAnime>;
