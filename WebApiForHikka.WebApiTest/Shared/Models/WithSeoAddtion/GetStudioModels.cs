using WebApiForHikka.Domain.Models.WithSeoAddition;
using WebApiForHikka.Dtos.Dto.WithSeoAddition.Studios;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

public class GetStudioModels
{
    public static Studio GetSample() => new()
    {
        Name = "Test",
        Logo = "Test",
        SeoAddition = GetSeoAdditionModels.GetSample(),
    };

    public static Studio GetSampleForUpdate() => new()
    {
        Name = "test1",
        Logo = "logo1",
        SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
    };
    public static CreateStudioDto GetCreateDtoSample()
    {
        return new CreateStudioDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            Logo = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetCreateDtoSample(),
        };
    }

    public static GetStudioDto GetGetDtoSample()
    {
        return new GetStudioDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            Logo = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetGetDtoSample(),
            Id = new Guid(),
        };
    }

    public static Studio GetModelSample()
    {
        return new Studio()
        {
            Name = Faker.Lorem.GetFirstWord(),
            Logo = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetSample(),
            Id = new Guid(),
        };
    }

    public static UpdateStudioDto GetUpdateDtoSample()
    {
        return new UpdateStudioDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            Logo = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetUpdateDtoSample(),
            Id = new Guid(),
        };
    }
}
