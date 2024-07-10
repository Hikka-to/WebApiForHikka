using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute.EntityValidationAttributes;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeBackdrops;

[ModelMetadataType(typeof(AnimeBackdrop))]
[ExportTsInterface]
public class UpdateAnimeBackdropDto : ModelDto
{
    [AnimeValidation] public required Guid AnimeId { get; set; }

    public required string Path { get; set; }

    public required int Width { get; set; }

    public required int Height { get; set; }

    public required List<int> Colors { get; set; }
}