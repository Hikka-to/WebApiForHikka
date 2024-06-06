using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.Mediaplayers;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Mediaplayers;

public class UpdateMediaplayerDto : ModelDto
{
    [StringLength(MediaplayerNumberConstants.NameLenght)]
    public required string Name { get; set; }

    [StringLength(MediaplayerNumberConstants.IconLenght)]
    public required string Icon { get; set; }

}
