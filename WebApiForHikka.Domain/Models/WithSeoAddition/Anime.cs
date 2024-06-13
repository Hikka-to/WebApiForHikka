using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.Animes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Domain.Models.WithSeoAddition;

public class Anime : ModelWithSeoAddition
{
    public ICollection<Tag> Tags { get; } = [];
    public ICollection<TagAnime> TagAnimes { get; } = [];

    public ICollection<Country> Countries { get; } = [];
    public ICollection<CountryAnime> CountryAnimes { get; } = [];

    public ICollection<Dub> Dubs { get; } = [];
    public ICollection<DubAnime> DubAnimes { get; } = [];

    public required Kind Kind { get; set; }

    public required Status Status { get; set; }

    public required Period Period { get; set; }

    public required RestrictedRating RestrictedRating { get; set; }

    public required Source Source { get; set; }

    [StringLength(AnimeNumberConstants.NameLenght)]
    public required string Name { get; set; }

    [StringLength(AnimeNumberConstants.ImageNameLenght)]
    public string? ImageName { get; set; }

    [StringLength(AnimeNumberConstants.RomajiNameLenght)]
    public string? RomajiName { get; set; }

    [StringLength(AnimeNumberConstants.NativeNameLenght)]
    public required string NativeName { get; set; }

    [StringLength(AnimeNumberConstants.PosterPathLenght)]
    public required string PosterPath { get; set; }

    public required ICollection<int> PosterColors { get; set; }

    [Range(0, float.MaxValue)]
    public required float AvgDuration;

    [Range(0, int.MaxValue)]
    public required int HowManyEpisodes;

    public required DateTime FirstAirDate;

    public required DateTime LastAirDate;

    public long? TmdbId { get; set; }
    public long? ShikimoriId { get; set; }


    [Range(AnimeNumberConstants.LowestScore, AnimeNumberConstants.MaxScore)]
    public required float ShikimoriScore { get; set; }

    [Range(AnimeNumberConstants.LowestScore, AnimeNumberConstants.MaxScore)]
    public required float TmdbScore { get; set; }
    
    [Range(AnimeNumberConstants.LowestScore, AnimeNumberConstants.MaxScore)]
    public required float ImdbScore { get; set; }

    [DefaultValue(false)]
    public required bool IsPublished { get; set; }

    public DateTime? PublishedAt { get; set; }

    public required DateTime UpdatedAt { get; set; }
    public required DateTime CreatedAt { get; set; }


    public string TagsString => string.Join(", ", Tags.Select(t => t.Name));
    public string CountriesString => string.Join(", ", Countries.Select(t => t.Name));
    public string DubsString => string.Join(", ", Dubs.Select(t => t.Name));

}
