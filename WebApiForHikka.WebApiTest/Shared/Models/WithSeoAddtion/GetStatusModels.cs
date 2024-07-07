using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.RestrictedRatings;
using WebApiForHikka.Dtos.Dto.Statuses;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

public class GetStatusModels
{
    public static Status GetSample() => new()
    {
        Name = "Test",
        SeoAddition = GetSeoAdditionModels.GetSample(),
    };

    public static Status GetSampleForUpdate() => new()
    {
        Name = "test1",
        SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
    };

    public static CreateStatusDto GetCreateDtoSample() 
    {
        return new CreateStatusDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetCreateDtoSample(),
        };
    }

    public static GetStatusDto GetGetDtoSample()
    {
        return new GetStatusDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetGetDtoSample(),
            Id = new Guid(),
        };
    }

    public static UpdateStatusDto GetUpdateDtoSample()
    {
        return new UpdateStatusDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetUpdateDtoSample(),
            Id = new Guid(),
        };
    }
}
