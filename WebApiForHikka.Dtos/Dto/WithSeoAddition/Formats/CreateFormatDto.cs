using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Formats;

public class CreateFormatDto : CreateDtoWithSeoAddition
{
    public required string Name { get; set; }
}
