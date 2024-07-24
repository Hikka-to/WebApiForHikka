using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithoutSeoAddition.CommentLikes;

public class CommentLikeRepositoryTest : SharedRepositoryTest<CommentLike, CommentLikeRepository>
{
    protected override CommentLike GetSample()
    {
        return GetCommentLikeModels.GetSample();
    }

    protected override CommentLike GetSampleForUpdate()
    {
        return GetCommentLikeModels.GetSampleForUpdate();
    }

    protected override CommentLikeRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new CommentLikeRepository(hikkaDbContext);
    }
}