using WebApiForHikka.Application.WithoutSeoAddition.CommentLikes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithoutSeoAddition.CommentLikes;

public class CommentLikeServiceTest : SharedServiceTest<CommentLike, CommentLikeService>
{
    protected override CommentLike GetSample()
    {
        return GetCommentLikeModels.GetSample();
    }

    protected override CommentLike GetSampleForUpdate()
    {
        return GetCommentLikeModels.GetSampleForUpdate();
    }

    protected override CommentLikeService GetService(HikkaDbContext hikkaDbContext)
    {
        return new CommentLikeService(new CommentLikeRepository(hikkaDbContext));
    }
}