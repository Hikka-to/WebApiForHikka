using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute.EntityValidationAttributes;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeVideos;

[ModelMetadataType(typeof(AnimeVideo))]
[ExportTsInterface(OutputDir = "./TS/Dto/WithoutSeoAddition/AnimeVideos")]
public class UpdateAnimeVideoDto : ModelDto
{
    [AnimeVideoKindValidation]
    public required Guid AnimeVideoKindId { get; set; }

    public required string Name { get; set; }

    public required string Url { get; set; }

    public required string ImageUrl { get; set; }

    public required string EmbedUrl { get; set; }
}
