using WebApiForHikka.Application.Commentables;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories;

public class CommentableRepository(HikkaDbContext dbContext)
    : CrudRepository<Commentable>(dbContext), ICommentableRepository;