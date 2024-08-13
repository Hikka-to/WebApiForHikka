namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

public class ReviewLike : Model
{
    public virtual required Review Review { get; set; }

    public virtual required User User { get; set; }

    public required bool IsLiked { get; set; }
}