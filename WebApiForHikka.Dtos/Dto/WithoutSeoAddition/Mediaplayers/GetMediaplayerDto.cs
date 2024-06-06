using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Mediaplayers;

public class GetMediaplayerDto : ModelDto
{
    public required string Name { get; set; }

    public required string Icon { get; set; }

}
