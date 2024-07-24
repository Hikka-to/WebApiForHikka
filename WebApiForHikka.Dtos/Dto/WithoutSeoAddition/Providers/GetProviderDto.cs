using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Animes;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Providers;

[ExportTsInterface]
public class GetProviderDto : ModelDto
{
    public required GetAnimeDto Anime { get; set; }

    public required string LogoPath { get; set; }

    public required string Name { get; set; }

    public required int Priority { get; set; }
}