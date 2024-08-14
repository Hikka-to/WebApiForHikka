using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Shared;

namespace WebApiForHikka.Domain.Models.WithSeoAddition;

public class Character : ModelWithSeoAddition
{
    public Guid Id { get; set; }

    [StringLength(SharedNumberConstatnts.NameLength)]
    public string? Name { get; set; }

    [StringLength(SharedNumberConstatnts.NameLength)]
    public required string RomajiName { get; set; }

    [StringLength(SharedNumberConstatnts.NameLength)]
    public required string NativeName { get; set; }

    [StringLength(SharedNumberConstatnts.NameLength)]
    public string? AlternativeName { get; set; }

    public required Guid AnimeId { get; set; }

    public required string ImagePath { get; set; }

    [StringLength(1024)]
    public string? Description { get; set; }

    public Guid? SeoAdditionId { get; set; }

    public required DateTime UpdatedAt { get; set; }

    public required DateTime CreatedAt { get; set; }

    public virtual Anime Anime { get; set; } = null!;
}