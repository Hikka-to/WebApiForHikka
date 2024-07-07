using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Kinds;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

public static class GetKindModels
{
    public static Kind GetSample() => new()
    {
        Hint = "test",
        Slug = "test",
        SeoAddition = GetSeoAdditionModels.GetSample(),
    };

    public static Kind GetSampleForUpdate() => new()
    {

        Hint = "test1",
        Slug = "test1",
        SeoAddition = GetSeoAdditionModels.GetSampleForUpdate(),
    };
    public static CreateKindDto GetCreateDtoSample()
    {
        return new CreateKindDto()
        {
            Hint = Faker.Lorem.GetFirstWord(),
            Slug = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetCreateDtoSample(),
        };
    }

    public static GetKindDto GetGetDtoSample()
    {
        return new GetKindDto()
        {
            Hint = Faker.Lorem.GetFirstWord(),
            Slug = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetGetDtoSample(),
            Id = new Guid(),
        };
    }

    public static UpdateKindDto GetUpdateDtoSample()
    {
        return new UpdateKindDto()
        {
            Hint = Faker.Lorem.GetFirstWord(),
            Slug = Faker.Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetUpdateDtoSample(),
            Id = new Guid(),
        };
    }
}
