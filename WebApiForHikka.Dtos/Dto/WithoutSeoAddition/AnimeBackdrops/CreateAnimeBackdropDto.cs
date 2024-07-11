using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute.EntityValidationAttributes;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeBackdrops;

[ModelMetadataType(typeof(AnimeBackdrop))]
[ExportTsClass(OutputDir = "./TS/Dto/WithoutSeoAddition/AnimeBackdrops")]
public class CreateAnimeBackdropDto
{
    [AnimeValidation] public required Guid AnimeId { get; set; }

    public required IFormFile Image { get; set; }

}