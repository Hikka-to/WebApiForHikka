namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

public class CommentLike : Model
{
    public virtual required Comment Comment { get; set; }
    public virtual required User User { get; set; }
    public required bool IsLiked { get; set; }
}