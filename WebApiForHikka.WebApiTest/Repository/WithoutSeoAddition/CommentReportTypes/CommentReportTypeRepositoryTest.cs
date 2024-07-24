using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithoutSeoAddition.CommentReportTypes;

public class CommentReportTypeRepositoryTest : SharedRepositoryTest<CommentReportType, CommentReportTypeRepository>
{
    protected override CommentReportType GetSample()
    {
        return GetCommentReportTypeModels.GetSample();
    }

    protected override CommentReportType GetSampleForUpdate()
    {
        return GetCommentReportTypeModels.GetSampleForUpdate();
    }

    protected override CommentReportTypeRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new CommentReportTypeRepository(hikkaDbContext);
    }
}