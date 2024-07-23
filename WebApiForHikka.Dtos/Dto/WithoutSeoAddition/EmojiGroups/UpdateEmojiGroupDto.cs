using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.EmojiGroups;

public class UpdateEmojiGroupDto : ModelDto
{
    public required string Name { get; set; }
    public required string Slug { get; set; }
}