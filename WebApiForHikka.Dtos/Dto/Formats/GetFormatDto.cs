using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Formats;

public class GetFormatDto : GetDtoWithSeoAddition
{
    public required string Name { get; set; }
}
