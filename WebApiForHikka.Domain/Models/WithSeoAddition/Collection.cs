using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithSeoAddition.Collections;

namespace WebApiForHikka.Domain.Models.WithSeoAddition;

public class Collection : ModelWithSeoAddition
{
    [StringLength(CollectionNumberConstants.NameLength)]
    public required string Name { get; set; }

    [StringLength(CollectionNumberConstants.DescriptionLength)]
    public required string Description { get; set; }

    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }

    public virtual ICollection<Anime> Animes { get; set; } = [];
}