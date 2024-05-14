using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Kinds;

public class GetKindDto : GetDtoWithSeoAddition
{
    public required string Slug { get; set; }

    public required string Hint { get; set; }
}
