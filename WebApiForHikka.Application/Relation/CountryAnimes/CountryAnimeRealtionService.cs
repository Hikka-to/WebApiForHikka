using WebApiForHikka.Application.Shared.Relation;
using WebApiForHikka.Domain.Models.ManyToMany;

namespace WebApiForHikka.Application.Relation.CountryAnimes;

public class CountryAnimeRealtionService : RelationCrudService<CountryAnime, ICountryAnimeRelationRepository>, ICountryAnimeRelationService
{
    public CountryAnimeRealtionService(ICountryAnimeRelationRepository repository) : base(repository)
    {
    }
}

