using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeVideoKinds;

[ExportTsClass(OutputDir = "./TS/Dto/WithoutSeoAddition/AnimeVideoKinds")]
public class GetAnimeVideoKindDto : ModelDto
{
    public required string Name { get; set; }
}