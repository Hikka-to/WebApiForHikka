using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Periods;

namespace WebApiForHikka.Domain.Models;

public class Period : ModelWithSeoAddition
{
    [StringLength(PeriodNumberConstants.NameLength)]
    public required string Name { get; set; }
}