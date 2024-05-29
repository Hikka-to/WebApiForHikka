using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Formats;

public class UpdateFormatDto : UpdateDtoWithSeoAddition
{

    [StringLength(SharedNumberConstatnts.NameLenght)]
    public required string Name { get; set; }
}