using Microsoft.AspNetCore.Http;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;
using WebApiForHikka.Dtos.MyOwnValidationAttribute.FileValidationAttributes;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Episodes;

public class UpdateEpisodeImageDto : ModelDto
{
    [EntityValidation<Episode>] public required Guid EpisodeId { get; set; }


    [FileContentType("image/*")]
    [MaxFileSize(SharedNumberConstatnts.MaxFileSize)]
    public required IFormFile Image { get; set; }

}
