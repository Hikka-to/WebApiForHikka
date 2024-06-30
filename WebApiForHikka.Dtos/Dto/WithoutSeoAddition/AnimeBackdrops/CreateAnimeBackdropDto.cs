using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute.EntityValidationAttributes;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeBackdrops;

[ModelMetadataType(typeof(AnimeBackdrop))]
[ExportTsClass(OutputDir = "./TS/Dto/WithoutSeoAddition/AnimeBackdrops")]
public class CreateAnimeBackdropDto
{
    [AnimeValidation]
    public required Guid AnimeId { get; set; }

    public required string Path { get; set; }

    public required int Width { get; set; }

    public required int Height { get; set; }

    public required List<int> Colors { get; set; }
}
