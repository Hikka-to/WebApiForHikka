using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Constants.Models.Periods;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Periods;

[ExportTsInterface(OutputDir = "./../admin-panel-hikka/models/Dto/WithSeoAddition/Periods")]
public class UpdatePeriodDto : UpdateDtoWithSeoAddition
{
    [StringLength(PeriodNumberConstants.NameLength)]
    public required string Name { get; set; }
}