using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.Studios;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Studios;

public class UpdateStudioDto : UpdateDtoWithSeoAddition
{
    [StringLength(StudioNumberConstants.NameLenght)]
    public required string Name { get; set; }

    [StringLength(StudioNumberConstants.LogoLenght)]
    public string? Logo { get; set; }

}

