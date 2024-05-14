using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Periods;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Periods;

public class CreatePeriodDto : CreateDtoWithSeoAddition
{
    [StringLength(PeriodNumberConstants.NameLenght)]
    public required string Name { get; set; }
}
