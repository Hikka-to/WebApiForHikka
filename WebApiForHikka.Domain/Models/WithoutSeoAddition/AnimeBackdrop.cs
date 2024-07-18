using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithoutSeoAddition.AnimeBackdrops;
using WebApiForHikka.Domain.Models.WithSeoAddition;

namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

public class AnimeBackdrop : Model
{
    public virtual required Anime Anime { get; set; }

    [StringLength(AnimeBackdropNumberConstants.PathLength)]
    public required string Path { get; set; }

    [Range(0, int.MaxValue)] public required int Width { get; set; }

    [Range(0, int.MaxValue)] public required int Height { get; set; }

    public List<int>? Colors { get; set; }
}