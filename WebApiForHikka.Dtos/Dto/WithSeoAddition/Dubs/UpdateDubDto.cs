using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Constants.Models.Dubs;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Dubs;

[ExportTsInterface(OutputDir = "./TS/Dto/WithSeoAddition/Dubs")]
public class UpdateDubDto : UpdateDtoWithSeoAddition
{

    [StringLength(DubNumberConstants.NameLength)]
    public required string Name { get; set; } 


    [StringLength(DubNumberConstants.IconLength)]
    public string? Icon { get; set; }
}
