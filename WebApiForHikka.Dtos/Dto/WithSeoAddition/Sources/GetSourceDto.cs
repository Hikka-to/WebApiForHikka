using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Sources;

public class GetSourceDto : GetDtoWithSeoAddition
{
    public required string Name { get; set; }
}