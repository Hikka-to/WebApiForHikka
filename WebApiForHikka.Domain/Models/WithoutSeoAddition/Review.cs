using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithoutSeoAddition.Review;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain.Models.Relation;

namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

public class Review : Model
{
    public required virtual AnimeRating AnimeRating { get; set; }
    
    [StringLength(SharedNumberConstatnts.NameLength)]
    public required string Name { get; set; }
    
    [StringLength(ReviewNumberConstants.BodyLength)]
    public required string Body { get; set; }
    
    public required DateTime UpdatedAt { get; set; }
    
    public required DateTime CreatedAt { get; set; }
    
    public required DateTime RemovedAt { get; set; }
}