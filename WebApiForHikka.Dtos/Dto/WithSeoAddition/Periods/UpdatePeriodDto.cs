using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Constants.Periods;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Periods;

[ExportTsInterface(OutputDir = "./TS/Dto/WithSeoAddition/Periods")]
public class UpdatePeriodDto : UpdateDtoWithSeoAddition
{
    [StringLength(PeriodNumberConstants.NameLenght)]
    public required string Name { get; set; }
}