using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Constants.Models.WithSeoAddition.Periods;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Periods;

[ExportTsInterface]
public class UpdatePeriodDto : UpdateDtoWithSeoAddition
{
    [StringLength(PeriodNumberConstants.NameLength)]
    public required string Name { get; set; }
}