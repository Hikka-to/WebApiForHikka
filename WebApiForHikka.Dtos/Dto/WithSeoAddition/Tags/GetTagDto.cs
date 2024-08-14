using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Tags;

[ExportTsInterface]
public class GetTagDto : GetDtoWithSeoAddition
{
    public required string Name { get; set; }

    public required string EngName { get; set; }

    public required List<string> Alises { get; set; }
    
    public  bool IsCharacterTag { get; set; } = false;    
    public required bool IsGenre { get; set; }

    public Guid? ParentTagId { get; set; }
}