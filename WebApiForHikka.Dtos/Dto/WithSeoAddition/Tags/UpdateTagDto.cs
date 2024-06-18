using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Tags;


[ExportTsInterface(OutputDir = "./TS/Dto/WithSeoAddition/Tags")]
public class UpdateTagDto : UpdateDtoWithSeoAddition
{

    [StringLength(SharedNumberConstatnts.NameLenght)]
    public required string Name { get; set; }

    [StringLength(SharedNumberConstatnts.NameLenght)]
    public required string EngName { get; set; }

    public required List<string> Alises { get; set; }

    public required bool IsGenre { get; set; }

    [TagValidation]
    public Guid? ParentTagId { get; set; }
}