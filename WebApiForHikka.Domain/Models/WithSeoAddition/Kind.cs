using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.Kinds;
using WebApiForHikka.Constants.Shared;

namespace WebApiForHikka.Domain.Models;

public class Kind : ModelWithSeoAddition
{
    [StringLength(SharedNumberConstatnts.SlugLenght)]
    public required string Slug { get; set; }

    [StringLength(KindNumberConstants.HintLenght)]
    public required string Hint { get; set; }
}