using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Shared;

namespace WebApiForHikka.Dtos.Dto.Relation.AnimeRatings;


[ExportTsInterface]
public class GetAnimeRatingDto : ModelDto
{

    public  Guid UserId { get; set; }
    public  Guid AnimeId { get; set; }

    public int Number { get; set; }

}
