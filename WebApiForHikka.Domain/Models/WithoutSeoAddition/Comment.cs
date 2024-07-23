using System.ComponentModel.DataAnnotations;
using WebApiForHikka.Constants.Models.WithoutSeoAddition.Comments;

namespace WebApiForHikka.Domain.Models.WithoutSeoAddition;

public class Comment : Commentable
{
    [MaxLength(CommentNumberConstants.BodyLength)]
    public required string Body { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public virtual required User User { get; set; }
    public virtual required Commentable Parent { get; set; }
}