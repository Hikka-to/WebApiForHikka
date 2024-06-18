using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Constants.Models.Kinds;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Kinds;

[ExportTsInterface(OutputDir = "./TS/Dto/WithSeoAddition/Kinds")]
public class CreateKindDto : CreateDtoWithSeoAddition
{
    
    [StringLength(SharedNumberConstatnts.SlugLenght)]
    public required string Slug { get; set; }

    [StringLength(KindNumberConstants.HintLenght)]
    public required string Hint { get; set; }
}