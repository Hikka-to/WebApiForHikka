using WebApiForHikka.Application.WithoutSeoAddition.Comments;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithoutSeoAddition.Comments;

public class CommentServiceTest : SharedServiceTest<Comment, CommentService>
{
    protected override Comment GetSample()
    {
        return GetCommentModels.GetSample();
    }

    protected override Comment GetSampleForUpdate()
    {
        return GetCommentModels.GetSampleForUpdate();
    }

    protected override CommentService GetService(HikkaDbContext hikkaDbContext)
    {
        return new CommentService(new CommentRepository(hikkaDbContext));
    }
}