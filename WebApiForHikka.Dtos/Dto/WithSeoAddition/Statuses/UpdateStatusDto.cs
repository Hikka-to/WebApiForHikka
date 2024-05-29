using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Statuses;
public class UpdateStatusDto : UpdateDtoWithSeoAddition
{

    [StringLength(SharedNumberConstatnts.NameLenght)]
    public required string Name { get; set; }
}