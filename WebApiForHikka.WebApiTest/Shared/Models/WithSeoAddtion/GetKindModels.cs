using Faker;
using WebApiForHikka.Domain.Models;
using WebApiForHikka.Dtos.Dto.Kinds;
using WebApiForHikka.Test.Shared.Models.WithoutSeoAddition;

namespace WebApiForHikka.Test.Shared.Models.WithSeoAddtion;

public static class GetKindModels
{
    public static Kind GetSample()
    {
        return new Kind
        {
            Hint = "test",
            Slug = "test",
            SeoAddition = GetSeoAdditionModels.GetSample()
        };
    }

    public static Kind GetSampleForUpdate()
    {
        return new Kind
        {
            Hint = "test1",
            Slug = "test1",
            SeoAddition = GetSeoAdditionModels.GetSampleForUpdate()
        };
    }

    public static CreateKindDto GetCreateDtoSample()
    {
        return new CreateKindDto
        {
            Hint = Lorem.GetFirstWord(),
            Slug = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetCreateDtoSample()
        };
    }

    public static GetKindDto GetGetDtoSample()
    {
        return new GetKindDto
        {
            Hint = Lorem.GetFirstWord(),
            Slug = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetGetDtoSample(),
            Id = new Guid()
        };
    }

    public static UpdateKindDto GetUpdateDtoSample()
    {
        return new UpdateKindDto
        {
            Hint = Lorem.GetFirstWord(),
            Slug = Lorem.GetFirstWord(),
            SeoAddition = GetSeoAdditionModels.GetUpdateDtoSample(),
            Id = new Guid()
        };
    }
}