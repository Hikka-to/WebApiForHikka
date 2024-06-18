using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Statuses;

[ExportTsInterface(OutputDir = "./TS/Dto/WithSeoAddition/Statuses")]
public class CreateStatusDto : CreateDtoWithSeoAddition
{
    [StringLength(SharedNumberConstatnts.NameLenght)]
    public required string Name { get; set; }
}