using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.Kinds;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Kinds;

public class CreateKindDto : CreateDtoWithSeoAddition
{
    
    [StringLength(SharedNumberConstatnts.SlugLenght)]
    public required string Slug { get; set; }

    [StringLength(KindNumberConstants.HintLenght)]
    public required string Hint { get; set; }
}