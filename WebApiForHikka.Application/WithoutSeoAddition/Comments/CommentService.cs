using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;

namespace WebApiForHikka.Application.WithoutSeoAddition.Comments;

public class CommentService(ICommentRepository repository)
    : CrudService<Comment, ICommentRepository>(repository), ICommentService;