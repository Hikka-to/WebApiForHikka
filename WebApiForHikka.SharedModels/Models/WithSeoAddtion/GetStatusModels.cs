using Faker;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Statuses;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

namespace WebApiForHikka.SharedModels.Models.WithSeoAddtion;

public class GetStatusModels
{
    public static Status GetSample()
    {
        return new Status
        {
            Name = "Test",
            SeoAddition = GetSeoAdditionModels.GetSample()
        };
    }

    public static Status GetSampleForUpdate()
    {
        return new Status
        {
            Name = "test1",
            SeoAddition = GetSeoAdditionModels.GetSampleForUpdate()
        };
    }

    public static CreateStatusDto GetCreateDtoSample()
    {
        return new CreateStatusDto
        {
            Name = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetCreateDtoSample()
        };
    }

    public static GetStatusDto GetGetDtoSample()
    {
        return new GetStatusDto
        {
            Name = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetGetDtoSample(),
            Id = new Guid()
        };
    }

    public static UpdateStatusDto GetUpdateDtoSample()
    {
        return new UpdateStatusDto
        {
            Name = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetUpdateDtoSample(),
            Id = new Guid()
        };
    }
}