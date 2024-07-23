using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Commentables;

public class CommentableService(ICommentableRepository repository)
    : CrudService<Commentable, ICommentableRepository>(repository), ICommentableService;