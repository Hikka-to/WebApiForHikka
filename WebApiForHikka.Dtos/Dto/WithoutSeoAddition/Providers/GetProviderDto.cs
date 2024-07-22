using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Animes;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Providers;

[ExportTsInterface]
public class GetProviderDto : Model
{
    public required GetAnimeDto Anime { get; set; }

    public required string LogoPath { get; set; }

    public required string Name { get; set; }

    public required int Priority { get; set; }
}