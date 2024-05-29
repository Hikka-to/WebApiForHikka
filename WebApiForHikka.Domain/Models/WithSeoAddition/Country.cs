using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.Countries;

namespace WebApiForHikka.Domain.Models.WithSeoAddition;

public class Country : ModelWithSeoAddition
{
    [StringLength(CountryNumberConstants.NameLenght)]
    public required string Name;

    [StringLength(CountryNumberConstants.IconLenght)]
    public required string Icon;
}
