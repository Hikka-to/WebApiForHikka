using WebApiForHikka.Application.WithoutSeoAddition.CommentLikes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;

namespace WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;

public class CommentLikeRepository(HikkaDbContext dbContext)
    : CrudRepository<CommentLike>(dbContext), ICommentLikeRepository;