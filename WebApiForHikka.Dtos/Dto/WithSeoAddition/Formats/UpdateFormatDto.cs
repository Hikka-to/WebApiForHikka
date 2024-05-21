using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Formats;

public class UpdateFormatDto : UpdateDtoWithSeoAddition
{
    public required string Name { get; set; }
}