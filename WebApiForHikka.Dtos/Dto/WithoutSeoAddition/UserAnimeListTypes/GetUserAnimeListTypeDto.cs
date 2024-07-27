using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.UserAnimeListTypes;

[ExportTsInterface]
public class GetUserAnimeListTypeDto : ModelDto
{
    public required string Slug { get; set; }

    public required string Name { get; set; }
}