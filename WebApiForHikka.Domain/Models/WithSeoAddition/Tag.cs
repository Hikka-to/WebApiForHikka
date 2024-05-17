using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiForHikka.Domain.Models.WithSeoAddition;

public class Tag : ModelWithSeoAddition
{

    public required string Name { get; set; }
    public required string EngName { get; set; }
    public required string Alises { get; set; }
    public required bool IsGenre { get; set; }

    [ForeignKey("ParentId")]
    public virtual Tag? ParentTag { get; set; }

    public virtual ICollection<Tag>? Tags { get; set; }

}
