using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Repository;

namespace WebApiForHikka.Test.Repository.WithoutSeoAddition.CommentReports;

public class CommentReportRepositoryTest : SharedRepositoryTest<CommentReport, CommentReportRepository>
{
    protected override CommentReport GetSample()
    {
        return GetCommentReportModels.GetSample();
    }

    protected override CommentReport GetSampleForUpdate()
    {
        return GetCommentReportModels.GetSampleForUpdate();
    }

    protected override CommentReportRepository GetRepository(HikkaDbContext hikkaDbContext)
    {
        return new CommentReportRepository(hikkaDbContext);
    }
}