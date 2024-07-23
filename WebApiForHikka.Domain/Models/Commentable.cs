using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Domain.Models;

public abstract class Commentable : Model
{
    public virtual ICollection<Comment> Comments { get; set; } = [];
}