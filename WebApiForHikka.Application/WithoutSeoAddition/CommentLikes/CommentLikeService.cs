using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.CommentLikes;

public class CommentLikeService(ICommentLikeRepository repository)
    : CrudService<CommentLike, ICommentLikeRepository>(repository), ICommentLikeService;