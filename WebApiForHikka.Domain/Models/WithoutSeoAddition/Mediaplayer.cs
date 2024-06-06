using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.Mediaplayers;

namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

public class Mediaplayer : Model
{
    [StringLength(MediaplayerNumberConstants.NameLenght)]
    public required string Name { get; set; }

    [StringLength(MediaplayerNumberConstants.IconLenght)]
    public required string Icon { get; set; }

}
