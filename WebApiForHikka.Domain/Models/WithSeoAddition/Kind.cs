using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithSeoAddition.Kinds;
using WebApiForHikka.Constants.Shared;

namespace WebApiForHikka.Domain.Models.WithSeoAddition;

public class Kind : ModelWithSeoAddition
{
    [StringLength(SharedNumberConstatnts.SlugLength)]
    public required string Slug { get; set; }

    [StringLength(KindNumberConstants.HintLength)]
    public required string Hint { get; set; }
}