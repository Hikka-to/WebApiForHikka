using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.Countries;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

public static class GetCountryModels
{
    public static Country GetSample()
    {
        return new Country()
        {
            Icon = "Icon",
            Name = "Name",
            SeoAddition = GetSeoAdditionModels.GetSample(),
        };
    }

    public static Country GetSampleForUpdate()
    {
        return new Country()
        {
            Icon = "Icon1",
            Name = "Name1",
            SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
        };
    }

    public static CreateCountryDto GetCreateDtoSample()
    {
        return new CreateCountryDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            Icon = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetCreateDtoSample(),
        };
    }

    public static GetCountryDto GetGetDtoSample()
    {
        return new GetCountryDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            Icon = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetGetDtoSample(),
            Id = new Guid(),
        };
    }
    public static UpdateCountryDto GetUpdateDtoSample()
    {
        return new UpdateCountryDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            Icon = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetUpdateDtoSample(),
            Id = new Guid(),
        };
    }

}
