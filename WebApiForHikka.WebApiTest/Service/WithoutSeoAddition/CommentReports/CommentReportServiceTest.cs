using WebApiForHikka.Application.WithoutSeoAddition.CommentReports;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithoutSeoAddition.CommentReports;

public class CommentReportServiceTest : SharedServiceTest<CommentReport, CommentReportService>
{
    protected override CommentReport GetSample()
    {
        return GetCommentReportModels.GetSample();
    }

    protected override CommentReport GetSampleForUpdate()
    {
        return GetCommentReportModels.GetSampleForUpdate();
    }

    protected override CommentReportService GetService(HikkaDbContext hikkaDbContext)
    {
        return new CommentReportService(new CommentReportRepository(hikkaDbContext));
    }
}