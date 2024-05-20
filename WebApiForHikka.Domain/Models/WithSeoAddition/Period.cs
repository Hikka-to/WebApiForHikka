using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Periods;

namespace WebApiForHikka.Domain.Models;

public class Period : ModelWithSeoAddition
{
    [StringLength(PeriodNumberConstants.NameLenght)]
    public required string Name { get; set; }
}