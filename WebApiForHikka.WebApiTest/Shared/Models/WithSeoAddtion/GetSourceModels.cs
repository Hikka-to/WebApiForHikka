using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Sources;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

public static class GetSourceModels
{
    public static Source GetSample() => new()
    {
        Name = "test",
        SeoAddition = GetSeoAdditionModels.GetSample(),
    };

    public static Source GetSampleForUpdate() => new()
    {
        Name = "test1",
        SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
    };

    public static CreateSourceDto GetCreateDtoSample()
    {
        return new CreateSourceDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetCreateDtoSample(),
        };
    }

    public static GetSourceDto GetGetDtoSample()
    {
        return new GetSourceDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetGetDtoSample(),
            Id = new Guid(),
        };
    }

    public static UpdateSourceDto GetUpdateDtoSample()
    {
        return new UpdateSourceDto()
        {
            Name = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetUpdateDtoSample(),
            Id = new Guid(),
        };
    }
}
