using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithSeoAddition.Periods;

namespace WebApiForHikka.Domain.Models.WithSeoAddition;

public class Period : ModelWithSeoAddition
{
    [StringLength(PeriodNumberConstants.NameLength)]
    public required string Name { get; set; }
}