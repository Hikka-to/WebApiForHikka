using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.ManyToMany;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Application.Relation.CountryAnimes;

public class CountryAnimeRealtionService :
    RelationCrudService<CountryAnime, Country, Anime, ICountryAnimeRelationRepository>, ICountryAnimeRelationService
{
    public CountryAnimeRealtionService(ICountryAnimeRelationRepository repository) : base(repository)
    {
    }
}