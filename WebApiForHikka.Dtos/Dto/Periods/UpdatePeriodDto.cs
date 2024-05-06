
using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Periods;
using WebApiForHikka.Dtos.Dto.SeoAddition;

namespace WebApiForHikka.Dtos.Dto.Periods;
public class UpdatePeriodDto
{
    public required Guid Id { get; set; }

    [StringLength(PeriodNumberConstants.NameLenght)]
    public required string Name { get; set; }
    public required GetSeoAdditionDto SeoAdditionDto { get; set; }
}
