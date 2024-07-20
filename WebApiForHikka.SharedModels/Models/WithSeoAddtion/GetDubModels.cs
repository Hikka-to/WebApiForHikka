using Faker;
using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Dubs;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;

namespace WebApiForHikka.SharedModels.Models.WithSeoAddtion;

public static class GetDubModels
{
    public static Dub GetSample()
    {
        return new Dub
        {
            Icon = "Icon",
            Name = "Name",
            SeoAddition = GetSeoAdditionModels.GetSample()
        };
    }

    public static Dub GetSampleForUpdate()
    {
        return new Dub
        {
            Icon = "Icon1",
            Name = "Name1",
            SeoAddition = GetSeoAdditionModels.GetSampleForUpdate()
        };
    }

    public static CreateDubDto GetCreateDtoSample()
    {
        return new CreateDubDto
        {
            Name = Lorem.GetFirstWord(),
            Icon = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetCreateDtoSample()
        };
    }

    public static GetDubDto GetGetDtoSample()
    {
        return new GetDubDto
        {
            Name = Lorem.GetFirstWord(),
            Icon = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetGetDtoSample(),
            Id = new Guid()
        };
    }

    public static Dub GetModelSample()
    {
        return new Dub
        {
            Name = Lorem.GetFirstWord(),
            Icon = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetSample(),
            Id = new Guid()
        };
    }

    public static UpdateDubDto GetUpdateDtoSample()
    {
        return new UpdateDubDto
        {
            Name = Lorem.GetFirstWord(),
            Icon = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetUpdateDtoSample(),
            Id = new Guid()
        };
    }
}