using Faker;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.CommentReportTypes;

namespace WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

public class GetCommentReportTypeModels
{
    public static CommentReportType GetSample()
    {
        return new CommentReportType
        {
            Slug = "Slug"
        };
    }

    public static CommentReportType GetSampleForUpdate()
    {
        return new CommentReportType
        {
            Slug = "Slug1"
        };
    }

    public static CreateCommentReportTypeDto GetCreateDtoSample()
    {
        return new CreateCommentReportTypeDto
        {
            Slug = Lorem.GetFirstWord()
        };
    }

    public static GetCommentReportTypeDto GetGetDtoSample()
    {
        return new GetCommentReportTypeDto
        {
            Slug = Lorem.GetFirstWord(),
            Id = Guid.NewGuid()
        };
    }

    public static UpdateCommentReportTypeDto GetUpdateDtoSample()
    {
        return new UpdateCommentReportTypeDto
        {
            Slug = Lorem.GetFirstWord(),
            Id = Guid.NewGuid()
        };
    }
}