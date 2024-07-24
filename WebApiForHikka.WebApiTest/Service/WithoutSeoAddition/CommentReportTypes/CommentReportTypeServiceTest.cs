using WebApiForHikka.Application.WithoutSeoAddition.CommentReportTypes;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.EfPersistence.Data;
using WebApiForHikka.EfPersistence.Repositories.WithoutSeoAddition;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using WebApiForHikka.Test.Shared.Service;

namespace WebApiForHikka.Test.Service.WithoutSeoAddition.CommentReportTypes;

public class CommentReportTypeServiceTest : SharedServiceTest<CommentReportType, CommentReportTypeService>
{
    protected override CommentReportType GetSample()
    {
        return GetCommentReportTypeModels.GetSample();
    }

    protected override CommentReportType GetSampleForUpdate()
    {
        return GetCommentReportTypeModels.GetSampleForUpdate();
    }

    protected override CommentReportTypeService GetService(HikkaDbContext hikkaDbContext)
    {
        return new CommentReportTypeService(new CommentReportTypeRepository(hikkaDbContext));
    }
}