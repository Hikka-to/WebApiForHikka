using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Domain.Models.WithSeoAddition;

public class Tag : ModelWithSeoAddition
{

    [StringLength(SharedNumberConstatnts.NameLength)]
    public required string Name { get; set; }

    [StringLength(SharedNumberConstatnts.NameLength)]
    public required string EngName { get; set; }
    public required List<string> Alises { get; set; }
    public required bool IsGenre { get; set; }

    public virtual Tag? ParentTag { get; set; }

    public virtual ICollection<Tag> Tags { get; } = [];

    public ICollection<Anime> Animes { get; } = [];
}