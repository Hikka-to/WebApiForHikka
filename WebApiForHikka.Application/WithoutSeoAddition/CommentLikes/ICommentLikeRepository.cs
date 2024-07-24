using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.CommentLikes;

public interface ICommentLikeRepository : ICrudRepository<CommentLike>;