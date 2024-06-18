using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Formats;


[ExportTsInterface(OutputDir = "./TS/Dto/WithSeoAddition/Formats")]
public class CreateFormatDto : CreateDtoWithSeoAddition
{
    [StringLength(SharedNumberConstatnts.NameLenght)]
    public required string Name { get; set; }
}