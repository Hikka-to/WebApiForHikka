using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.Relation.AnimeRatings;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Domain.Models.Relation;

public class AnimeRating : RelationModel<User, Anime>
{
    [Range(0, AnimeRatingNumberConstants.MaxRating)]
    public required int Number { get; set; }

    public required DateTime CreateAt { get; set; }

    public required DateTime UpdateAt { get; set; }
}