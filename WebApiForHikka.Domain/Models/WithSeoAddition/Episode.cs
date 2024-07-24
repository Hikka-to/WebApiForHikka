using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiForHikka.Constants.Models.WithSeoAddition.Episodes;

namespace WebApiForHikka.Domain.Models.WithSeoAddition;

public class Episode : Commentable, IModelWithSeoAddition
{
    [ForeignKey(nameof(Anime))] public Guid AnimeId { get; set; }


    [StringLength(EpisodeNumberConstants.NameLenght)]
    public required string Name { get; set; }

    [Range(0, double.PositiveInfinity)] public required int Duration { get; set; }

    public required DateTime AirDate { get; set; }

    [DefaultValue(false)] public required bool IsFiller { get; set; } = false;

    public required DateTime UpdatedAt { get; set; }
    public required DateTime CreatedAt { get; set; }

    public virtual required SeoAddition SeoAddition { get; set; }
}