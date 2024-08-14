using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithSeoAddition.Characters;

[ExportTsInterface]
public class GetCharacterDto : GetDtoWithSeoAddition
{
    public string? Name { get; set; }

    public required string RomajiName { get; set; }

    public required string NativeName { get; set; }

    public string? AlternativeName { get; set; }

    public required Guid AnimeId { get; set; }

    public required string ImagePath { get; set; }

    public string? Description { get; set; }

    public required DateTime UpdatedAt { get; set; }

    public required DateTime CreatedAt { get; set; }
}