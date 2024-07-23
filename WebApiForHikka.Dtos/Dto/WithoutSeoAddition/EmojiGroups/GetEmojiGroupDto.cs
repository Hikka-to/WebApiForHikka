using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.EmojiGroups;

[ExportTsInterface]
public class GetEmojiGroupDto : ModelDto
{
    public required string Name { get; set; }
    public required string Slug { get; set; }
}