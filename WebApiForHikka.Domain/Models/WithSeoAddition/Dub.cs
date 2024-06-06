using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.Dubs;

namespace WebApiForHikka.Domain.Models.WithSeoAddition;

public class Dub : ModelWithSeoAddition
{
    [StringLength(DubNumberConstants.NameLenght)]
    public required string Name { get; set; }

    [StringLength(DubNumberConstants.IconLenght)]
    public string? Icon { get; set; }
}
