using Faker;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Studios;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

namespace WebApiForHikka.SharedModels.Models.WithSeoAddtion;

public class GetStudioModels
{
    public static Studio GetSample()
    {
        return new Studio
        {
            Name = "Test",
            Logo = "Test",
            SeoAddition = GetSeoAdditionModels.GetSample()
        };
    }

    public static Studio GetSampleForUpdate()
    {
        return new Studio
        {
            Name = "test1",
            Logo = "logo1",
            SeoAddition = GetSeoAdditionModels.GetSampleForUpdate()
        };
    }

    public static CreateStudioDto GetCreateDtoSample()
    {
        return new CreateStudioDto
        {
            Name = Lorem.GetFirstWord(),
            Logo = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetCreateDtoSample()
        };
    }

    public static GetStudioDto GetGetDtoSample()
    {
        return new GetStudioDto
        {
            Name = Lorem.GetFirstWord(),
            Logo = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetGetDtoSample(),
            Id = new Guid()
        };
    }

    public static Studio GetModelSample()
    {
        return new Studio
        {
            Name = Lorem.GetFirstWord(),
            Logo = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetSample(),
            Id = new Guid()
        };
    }

    public static UpdateStudioDto GetUpdateDtoSample()
    {
        return new UpdateStudioDto
        {
            Name = Lorem.GetFirstWord(),
            Logo = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetUpdateDtoSample(),
            Id = new Guid()
        };
    }
}