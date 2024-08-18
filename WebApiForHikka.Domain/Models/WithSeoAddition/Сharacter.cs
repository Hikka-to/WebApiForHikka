using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithSeoAddition.Characters;
using WebApiForHikka.Domain.Models.Relation;

namespace WebApiForHikka.Domain.Models.WithSeoAddition;

public class Character : ModelWithSeoAddition
{
    [StringLength(CharacterNumberConstants.NameLength)]
    public string? Name { get; set; }

    [StringLength(CharacterNumberConstants.RomajiNameLength)]
    public required string RomajiName { get; set; }

    [StringLength(CharacterNumberConstants.NativeNameLength)]
    public required string NativeName { get; set; }

    [StringLength(CharacterNumberConstants.AlternativeNameLength)]
    public string? AlternativeName { get; set; }

    [StringLength(CharacterNumberConstants.ImagePathLength)]
    public required string ImagePath { get; set; }

    [StringLength(CharacterNumberConstants.DescriptionLength)]
    public string? Description { get; set; }

    public required DateTime UpdatedAt { get; set; }

    public required DateTime CreatedAt { get; set; }

    public virtual ICollection<Anime> Animes { get; set; } = [];
    public virtual ICollection<Tag> Tags { get; set; } = [];

    public virtual ICollection<TagCharacter> TagCharacters { get; set; } = [];
    public virtual ICollection<AnimeCharacter> AnimeCharacters { get; set; } = [];
}