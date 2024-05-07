using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Periods;
using WebApiForHikka.Dtos.Dto.SeoAddition;

namespace WebApiForHikka.Dtos.Dto.Periods;

public class CreatePeriodDto
{
    [StringLength(PeriodNumberConstants.NameLenght)]
    public required string Name { get; set; }
    public required Guid SeoAdditionId { get; set; }
}
