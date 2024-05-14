
using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Periods;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Periods;
public class UpdatePeriodDto : UpdateDtoWithSeoAddition
{

    [StringLength(PeriodNumberConstants.NameLenght)]
    public required string Name { get; set; }
}
