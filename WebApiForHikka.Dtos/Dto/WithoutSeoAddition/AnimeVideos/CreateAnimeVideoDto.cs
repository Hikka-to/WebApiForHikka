using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute.EntityValidationAttributes;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeVideos;

[ModelMetadataType(typeof(AnimeVideo))]
[ExportTsInterface(OutputDir = "./../admin-panel-hikka/models/Dto/WithoutSeoAddition/AnimeVideos")]
public class CreateAnimeVideoDto
{
    [AnimeVideoKindValidation] public required Guid AnimeVideoKindId { get; set; }

    public required string Name { get; set; }

    public required string Url { get; set; }

    public required string ImageUrl { get; set; }

    public required string EmbedUrl { get; set; }
}