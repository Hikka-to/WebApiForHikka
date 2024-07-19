using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;
using WebApiForHikka.Dtos.MyOwnValidationAttribute.FileValidationAttributes;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Episodes;


[ModelMetadataType(typeof(EpisodeImage))]
public class CreateEpisodeImageDto 
{
    [EntityValidation<Episode>] public required Guid EpisodeId { get; set; }


    [FileContentType("image/*")]
    [MaxFileSize(SharedNumberConstatnts.MaxFileSize)]
    public required IFormFile Image { get; set; }

}
