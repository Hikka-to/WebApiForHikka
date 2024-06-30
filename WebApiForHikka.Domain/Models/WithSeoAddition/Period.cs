using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.Periods;

namespace WebApiForHikka.Domain.Models;

public class Period : ModelWithSeoAddition
{
    [StringLength(PeriodNumberConstants.NameLength)]
    public required string Name { get; set; }
}