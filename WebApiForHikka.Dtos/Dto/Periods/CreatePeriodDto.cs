using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Periods;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.SeoAddition;

namespace WebApiForHikka.Dtos.Dto.Periods;

public class CreatePeriodDto : ModelWithSeoAddition
{
    [StringLength(PeriodNumberConstants.NameLenght)]
    public required string Name { get; set; }
}
