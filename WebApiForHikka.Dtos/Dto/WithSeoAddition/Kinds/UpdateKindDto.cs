using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.Kinds;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Kinds;

public class UpdateKindDto : UpdateDtoWithSeoAddition
{
    public required string Slug { get; set; }

    [StringLength(KindNumberConstants.HintLenght)]
    public required string Hint { get; set; }
}