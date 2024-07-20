using Faker;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Countries;
using WebApiForHikka.SharedModels.Models.WithoutSeoAddition;
using Country = WebApiForHikka.Domain.Models.WithSeoAddition.Country;

namespace WebApiForHikka.SharedModels.Models.WithSeoAddtion;

public static class GetCountryModels
{
    public static Country GetSample()
    {
        return new Country
        {
            Icon = "Icon",
            Name = "Name",
            SeoAddition = GetSeoAdditionModels.GetSample()
        };
    }

    public static Country GetSampleForUpdate()
    {
        return new Country
        {
            Icon = "Icon1",
            Name = "Name1",
            SeoAddition = GetSeoAdditionModels.GetSampleForUpdate()
        };
    }

    public static CreateCountryDto GetCreateDtoSample()
    {
        return new CreateCountryDto
        {
            Name = Lorem.GetFirstWord(),
            Icon = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetCreateDtoSample()
        };
    }

    public static GetCountryDto GetGetDtoSample()
    {
        return new GetCountryDto
        {
            Name = Lorem.GetFirstWord(),
            Icon = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetGetDtoSample(),
            Id = new Guid()
        };
    }

    public static UpdateCountryDto GetUpdateDtoSample()
    {
        return new UpdateCountryDto
        {
            Name = Lorem.GetFirstWord(),
            Icon = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetUpdateDtoSample(),
            Id = new Guid()
        };
    }
}