using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Constants.Models.Mediaplayers;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Mediaplayers;

[ExportTsInterface(OutputDir = "./TS/Dto/WithoutSeoAddition/Mediaplayers")]
public class CreateMediaplayerDto
{
    [StringLength(MediaplayerNumberConstants.NameLenght)]
    public required string Name { get; set; }

    [StringLength(MediaplayerNumberConstants.IconLenght)]
    public required string Icon { get; set; }

}
