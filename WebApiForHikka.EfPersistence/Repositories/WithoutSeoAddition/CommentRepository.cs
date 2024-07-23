using WebApiForHikka.Application.WithoutSeoAddition.Comments;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class CommentRepository(HikkaDbContext dbContext) : CrudRepository<Comment>(dbContext), ICommentRepository;