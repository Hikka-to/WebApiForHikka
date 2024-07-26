using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Dtos.Dto.Relation.AnimeRatings;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Reviews;

[ExportTsInterface]
public class GetReviewDto
{
    public required GetAnimeRatingDto AnimeRating { get; set; }

    public required string Name { get; set; }

    public required string Body { get; set; }

    public required DateTime UpdatedAt { get; set; }

    public required DateTime CreatedAt { get; set; }

    public required DateTime RemovedAt { get; set; }
}