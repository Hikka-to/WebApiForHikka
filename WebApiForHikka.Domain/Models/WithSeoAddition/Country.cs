using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.Countries;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Domain.Models.WithSeoAddition;

public class Country : ModelWithSeoAddition
{
    [StringLength(CountryNumberConstants.NameLenght)]
    public required string Name;

    [StringLength(CountryNumberConstants.IconLenght)]
    public required string Icon;

    public ICollection<Anime> Animes { get; } = [];
    public ICollection<CountryAnime> CountryAnimes { get; } = [];
}
