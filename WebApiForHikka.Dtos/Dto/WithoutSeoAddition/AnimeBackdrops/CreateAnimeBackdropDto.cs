using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;
using WebApiForHikka.Dtos.MyOwnValidationAttribute.FileValidationAttributes;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.AnimeBackdrops;

[ModelMetadataType(typeof(AnimeBackdrop))]
public class CreateAnimeBackdropDto
{
    [EntityValidation<Anime>] public required Guid AnimeId { get; set; }

    [FileContentType("image/*")]
    [MaxFileSize(SharedNumberConstatnts.MaxFileSize)]
    public required IFormFile Image { get; set; }

}