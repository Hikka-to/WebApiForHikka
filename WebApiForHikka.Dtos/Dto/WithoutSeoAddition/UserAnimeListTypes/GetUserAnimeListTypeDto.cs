using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.UserAnimeListTypes;

[ExportTsInterface]
public class GetUserAnimeListTypeDto : Model
{
    public required string Slug { get; set; }

    public required string Name { get; set; }
}