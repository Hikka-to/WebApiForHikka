using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.ManyToMany;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.CountryAnimes;

public class CountryAnimeRelationService :
    RelationCrudService<CountryAnime, Country, Anime, ICountryAnimeRelationRepository>, ICountryAnimeRelationService
{
    public CountryAnimeRelationService(ICountryAnimeRelationRepository repository) : base(repository)
    {
    }
}