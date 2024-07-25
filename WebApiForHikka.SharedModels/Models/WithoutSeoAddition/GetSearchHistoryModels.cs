using Faker;
using WebApiForHikka.Domain.Models.WithoutSeoAddition;
using WebApiForHikka.Dtos.Dto.WithoutSeoAddition.SearchHistories;

namespace WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

public class GetSearchHistoryModels
{
    public static SearchHistory GetSample()
    {
        return new SearchHistory
        {
            CreateAt = DateTime.Now,
            Query = "test"
        };
    }

    public static SearchHistory GetSampleForUpdate()
    {
        return new SearchHistory
        {
            CreateAt = DateTime.Now,
            Query = "test1"
        };
    }

    public static CreateSearchHistoryDto GetCreateDtoSample()
    {
        return new CreateSearchHistoryDto
        {
            Query = Lorem.GetFirstWord(),
        };
    }

    public static GetSearchHistoryDto GetGetDtoSample()
    {
        return new GetSearchHistoryDto
        {
            Id = new Guid(),
            CreateAt = DateTime.Now,
            Query = "test1"
            
        };
    }

    public static SearchHistory GetModelSample()
    {
        return new SearchHistory
        {
            Id = new Guid(),
            CreateAt = new DateTime(),
            Query = "test1"
        };
    }

    public static UpdateSearchHistoryDto GetUpdateDtoSample()
    {
        return new UpdateSearchHistoryDto
        {
            Id = new Guid(),
            CreateAt = new DateTime(),
            Query = "test3"
        };
    }
}
