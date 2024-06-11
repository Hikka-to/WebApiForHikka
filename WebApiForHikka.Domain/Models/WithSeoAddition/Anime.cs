using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Domain.Models.WithSeoAddition;

public class Anime : ModelWithSeoAddition
{
    public ICollection<Tag> Tags { get; } = [];
    public ICollection<TagAnime> TagAnimes { get; } = [];

    public ICollection<Country> Countries { get; } = [];
    public ICollection<CountryAnime> CountryAnimes { get; } = [];

    public ICollection<Dub> Dubs { get; } = [];
    public ICollection<DubAnime> DubAnimes { get; } = [];
}
