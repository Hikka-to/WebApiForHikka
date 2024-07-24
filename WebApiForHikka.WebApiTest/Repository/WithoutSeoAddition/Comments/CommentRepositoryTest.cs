using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithoutSeoAddition.Comments;

public class CommentRepositoryTest : SharedRepositoryTest<Comment, CommentRepository>
{
    protected override Comment GetSample()
    {
        return GetCommentModels.GetSample();
    }

    protected override Comment GetSampleForUpdate()
    {
        return GetCommentModels.GetSampleForUpdate();
    }

    protected override CommentRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new CommentRepository(hikkaDbContext);
    }
}