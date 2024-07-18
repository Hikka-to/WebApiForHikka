using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithoutSeoAddition.AlternativeNames;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

public class AlternativeName : Model
{
    [StringLength(AlternativeNameNumberConstants.NameLength)]
    public required string Name { get; set; }

    public required Anime Anime { get; set; }
}