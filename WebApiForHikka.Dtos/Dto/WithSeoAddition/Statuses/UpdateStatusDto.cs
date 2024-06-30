using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Statuses;

[ExportTsInterface(OutputDir = "./TS/Dto/WithSeoAddition/Statuses")]
public class UpdateStatusDto : UpdateDtoWithSeoAddition
{

    [StringLength(SharedNumberConstatnts.NameLength)]
    public required string Name { get; set; }
}