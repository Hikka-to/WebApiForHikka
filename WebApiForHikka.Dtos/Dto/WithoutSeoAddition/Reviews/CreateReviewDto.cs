using Microsoft.AspNetCore.Mvc;
using TypeGen.Core.TypeAnnotations;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.MyOwnValidationAttribute;

namespace WebApiForHikka.Dtos.Dto.WithoutSeoAddition.Reviews;

[ModelMetadataType(typeof(Review))]
[ExportTsInterface]
public class CreateReviewDto
{
    [EntityValidation<Review>] public required Guid AnimeRatingId { get; set; }

    public required string Name { get; set; }

    public required string Body { get; set; }

    public required DateTime UpdatedAt { get; set; }

    public required DateTime CreatedAt { get; set; }

    public required DateTime RemovedAt { get; set; }
}