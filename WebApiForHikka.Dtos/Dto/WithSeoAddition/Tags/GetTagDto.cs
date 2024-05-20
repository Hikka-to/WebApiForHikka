using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Tags;

public class GetTagDto : GetDtoWithSeoAddition
{
    public required string Name { get; set; }

    public required string EngName { get; set; }

    public required string Alises { get; set; }

    public required bool IsGenre { get; set; }

    public Guid? ParentTagId { get; set; }
}