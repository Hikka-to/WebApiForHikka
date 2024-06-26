using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Constants.Models.AnimeBackdrops;
using WebApiForHikka.Dtos.MyOwnValidationAttribute.EntityValidationAttributes;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeBackdrops;

[ExportTsClass(OutputDir = "./TS/Dto/WithoutSeoAddition/AnimeBackdrops")]
public class UpdateAnimeBackdropDto : ModelDto
{
    [AnimeValidation]
    public required Guid AnimeId { get; set; }

    [StringLength(AnimeBackdropNumberConstants.PathLength)]
    public required string Path { get; set; }

    [Range(0, int.MaxValue)]
    public required int Width { get; set; }

    [Range(0, int.MaxValue)]
    public required int Height { get; set; }

    public required List<int> Colors { get; set; }
}
