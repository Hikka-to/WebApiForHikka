using Faker;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Periods;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

namespace WebApiForHikka.SharedModels.Models.WithSeoAddtion;

public static class GetPeriodModels
{
    public static Period GetSample()
    {
        return new Period
        {
            Name = "test",
            SeoAddition = GetSeoAdditionModels.GetSample()
        };
    }

    public static Period GetSampleForUpdate()
    {
        return new Period
        {
            Name = "test1",
            SeoAddition = GetSeoAdditionModels.GetSampleForUpdate()
        };
    }

    public static CreatePeriodDto GetCreateDtoSample()
    {
        return new CreatePeriodDto
        {
            Name = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetCreateDtoSample()
        };
    }

    public static GetPeriodDto GetGetDtoSample()
    {
        return new GetPeriodDto
        {
            Name = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetGetDtoSample(),
            Id = new Guid()
        };
    }

    public static UpdatePeriodDto GetUpdateDtoSample()
    {
        return new UpdatePeriodDto
        {
            Name = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetUpdateDtoSample(),
            Id = new Guid()
        };
    }
}