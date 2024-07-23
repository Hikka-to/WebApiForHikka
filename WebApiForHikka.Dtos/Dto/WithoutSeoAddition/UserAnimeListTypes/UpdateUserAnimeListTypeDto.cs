using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.UserAnimeListTypes;

public class UpdateUserAnimeListTypeDto : ModelDto
{
    public required string Slug { get; set; }
    
    public required string Name { get; set; }
}