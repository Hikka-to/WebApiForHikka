using Faker;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.CommentReports;

namespace WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

public class GetCommentReportModels
{
    public static CommentReport GetSample()
    {
        return new CommentReport
        {
            Comment = GetCommentModels.GetSample(),
            User = GetUserModels.GetSample(),
            CommentReportType = GetCommentReportTypeModels.GetSample(),
            Body = "Test"
        };
    }

    public static CommentReport GetSampleForUpdate()
    {
        return new CommentReport
        {
            Comment = GetCommentModels.GetSampleForUpdate(),
            User = GetUserModels.GetSampleForUpdate(),
            CommentReportType = GetCommentReportTypeModels.GetSampleForUpdate(),
            Body = "Test1"
        };
    }

    public static CreateCommentReportDto GetCreateDtoSample()
    {
        return new CreateCommentReportDto
        {
            CommentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CommentReportTypeId = Guid.NewGuid(),
            Body = Lorem.GetFirstWord()
        };
    }

    public static UpdateCommentReportDto GetUpdateDtoSample()
    {
        return new UpdateCommentReportDto
        {
            CommentId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CommentReportTypeId = Guid.NewGuid(),
            Body = Lorem.GetFirstWord(),
            Id = Guid.NewGuid()
        };
    }

    public static GetCommentReportDto GetGetDtoSample()
    {
        return new GetCommentReportDto
        {
            Comment = GetCommentModels.GetGetDtoSample(),
            User = GetUserModels.GetGetDtoSample(),
            CommentReportType = GetCommentReportTypeModels.GetGetDtoSample(),
            Body = Lorem.GetFirstWord(),
            Id = Guid.NewGuid()
        };
    }
}