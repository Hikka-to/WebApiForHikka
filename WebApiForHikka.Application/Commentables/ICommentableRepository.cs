using WebApiForHikka.Application.Shared;
using WebApiForHikka.Domain.Models;

namespace WebApiForHikka.Application.Commentables;

public interface ICommentableRepository : ICrudRepository<Commentable>;